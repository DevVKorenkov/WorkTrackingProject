using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Install_Printers_Lib.Actions;
using Microsoft.AspNetCore.Mvc;

namespace WorkTrackingSite.Attributes
{
    public class NameFilterAttribute : Attribute, IAsyncActionFilter
    {
        string _printerName;

        MultipartReader _reader;

        MultipartSection _section;

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            await CheckName(context);

            await next();
        }

        async Task CheckName(ActionExecutingContext context)
        {
            var boundary = HeaderUtilities.RemoveQuotes(MediaTypeHeaderValue.Parse(context.HttpContext.Request.ContentType).Boundary).Value;

            try
            {
                _reader = new MultipartReader(boundary, context.HttpContext.Request.Body);

                _section = await _reader.ReadNextSectionAsync();

                _printerName = await _section.ReadAsStringAsync();
            }
            catch (Exception e)
            {
                context.ActionArguments["requestResult"] = true;
            }

            Use_Install_Printers_Api api = new Use_Install_Printers_Api();

            var tempCol = await api.GetPrinters();

            var tempPrinterName = tempCol.Select(x => x.PrinterName == _printerName).ToList();

            if (tempPrinterName.Contains(true))
            {
                _reader = null;

                _section = null;

                context.ActionArguments["requestResult"] = true;
            }
        }
    }
}
