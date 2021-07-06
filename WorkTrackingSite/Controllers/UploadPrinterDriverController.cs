using Install_Printers_Lib.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using WorkTrackingSite.Attributes;
using System.Threading;

namespace WorkTrackingSite.Controllers
{
    [Route("UploadPrinterDriver/[controller]")]
    public class UploadPrinterDriverController : Controller
    {
        Use_Install_Printers_Api api;

        ILogger log;

        MultipartReader _reader;

        MultipartSection _section;

        Dictionary<string, string> _tempSectionsCol;

        public UploadPrinterDriverController(ILogger<UploadPrinterDriverController> logger)
        {
            api = new Use_Install_Printers_Api();

            log = logger;
        }

        [HttpGet]
        public async Task<IActionResult> UploadPrinterDriver()
        {
            ViewBag.message = "Ожидание загрузки файла";

            return View(await api.GetPrinters());
        }

        [HttpPost, DisableFormValueModelBinding, RequestSizeLimit(1_000_000_000)]
        public async Task<IActionResult> UploadFile()
        {
            HttpContext.Features.Get<IHttpResponseBodyFeature>().DisableBuffering();

            await GetSections();

            if (!await api.CheckName(_tempSectionsCol["PrinterName"]))
            {
                if (CheckExtention())
                {
                    if (_tempSectionsCol["NetPrinter"].Contains("on")) _tempSectionsCol["NetPrinter"] = "true";
                    else _tempSectionsCol["NetPrinter"] = "false";

                    var uploadFile = _section.AsFileSection();

                    var formDataContent = new MultipartFormDataContent();

                    try
                    {
                        formDataContent.Add(new StringContent(_tempSectionsCol["PrinterName"]), "PrinterName");
                        formDataContent.Add(new StringContent(_tempSectionsCol["NetPrinter"]), "NetPrinter");
                        formDataContent.Add(new StreamContent(uploadFile.FileStream), "uploadFile");
                    }
                    catch (Exception e)
                    {
                        log.LogError($"ошибка {e.Message}\n");

                        ViewBag.message = $"Ошибка - {e.Message}";
                    }

                    log.LogWarning($"Длина формы {formDataContent.Count()}");

                    api = new Use_Install_Printers_Api(formDataContent);

                    var result = await api.UploadNewDriverRequest();

                    if (result.StatusCode == HttpStatusCode.OK)
                        ViewBag.message = $"Файл {uploadFile.FileName} успешно загружен.";
                    else
                        ViewBag.message = $"Ошибка загрузки файла {uploadFile.FileName}.";

                    log.LogWarning($"{result.StatusCode}\n");
                }
                else
                {
                    ViewBag.message = $"Ошибка. Архив имел неверный формат.";
                }
            }
            else
            {
                ViewBag.message = $"Ошибка. Такой принтер уже есть в базе данных.";
            }

            return View("UploadPrinterDriver", await api.GetPrinters());
        }

        public async Task<bool> CheckName()
        {
            bool result = false;

            var response = await api.CheckName(_tempSectionsCol["PrinterName"]);

            if (!response)
            {
                result = true;
            }
            else
            {
                RedirectPage();
            }

            #region _
            //var tempCol = await api.GetPrinters();

            //var tempPrinterName = tempCol.Where(x => x.PrinterName == _tempSectionsCol["PrinterName"]).ToList();

            //if (tempPrinterName.Count() != 0)
            //{
            //    result = false;
            //}
            //else
            //{
            //    result = true;
            //}
            #endregion

            return result;
        }

        private IActionResult RedirectPage()
        {
            return View("UploadPrinterDriver", api.GetPrinters());
        }

        private bool CheckExtention()
        {
            bool temp;

            if (_tempSectionsCol["uploadFile"].Contains(".zip"))
            {
                temp = true;
            }
            else
            {
                temp = false;
            }

            return temp;
        }

        private async Task GetSections()
        {
            _tempSectionsCol = new Dictionary<string, string>();

            try
            {
                var boundary = HeaderUtilities.RemoveQuotes(MediaTypeHeaderValue.Parse(Request.ContentType).Boundary).Value;

                _reader = new MultipartReader(boundary, Request.Body);

                _section = await _reader.ReadNextSectionAsync();
                _tempSectionsCol.Add("PrinterName", await _section.ReadAsStringAsync());

                _section = await _reader.ReadNextSectionAsync();
                _tempSectionsCol.Add("NetPrinter", await _section.ReadAsStringAsync());

                _section = await _reader.ReadNextSectionAsync();
                _tempSectionsCol.Add("uploadFile", _section.AsFileSection().FileName);
            }
            catch (Exception e)
            {
                ViewBag.message = $"{e.Message}";
            }
        }
    }
}