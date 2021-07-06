using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WorkTrackingLib;
using WorkTrackingLib.Models;

namespace NewWorkTracking.Actions
{
    /// <summary>
    /// Contains the logic for loading and unloading data into an Excel file 
    /// </summary>
    class ExcelUsage
    {
        /// <summary>
        /// Compiles an Excel file
        /// </summary>
        public static bool GetExcel(List<NewWrite> temp, string path)
        {
            // Create a new Excel workbook 
            var wb = new XLWorkbook();

            // Initializing the interface for adding data to an Excel file 
            var ws = wb.Worksheets.Add("Data");

            try
            {
                // Condition for saving the file 
                if (path != null)
                {
                    ws.Row(1).Delete();

                    // Writing data to an Excel file 
                    ws.Cell(1, 1).InsertTable(temp);

                    // Removing columns with unnecessary information for the user 
                    ws.Column("V").Delete();
                    ws.Column("U").Delete();
                    ws.Column("T").Delete();
                    ws.Column("A").Delete();

                    // Retrieving Object Properties to Retrieve Attributes
                    var t = temp.FirstOrDefault().GetType().GetProperties().ToList();

                    // Loop for getting property and setting column headings 
                    for (var i = 1; i < t.Count; i++)
                    {
                        // Condition retrieves custom property attributes if they are not empty 
                        if (t[i].GetCustomAttribute<DisplayAttribute>() != null)
                        {
                            // Writing extracted property attributes to header cells
                            ws.Cell(1, i).SetValue(t[i].GetCustomAttribute<DisplayAttribute>().Name);
                        }
                    }

                    // Set the width of the cell to match the width of the text 
                    ws.Columns().AdjustToContents();

                    // Disable default auto filter 
                    ws.RangeUsed().SetAutoFilter(false);

                    // Save the file 
                    wb.SaveAs(path);

                    // Indicate the result of executing actions with the file upon successful saving 
                    return true;
                }
                else
                {
                    // Indicate the result of performing actions with the file when canceling the save 
                    return false;
                }
            }
            catch
            {
                //Message.Show("Ошибка", ex.Message, MessageBoxButton.OK);
                // Indicate the result of executing actions with the file if saving error 
                return false;
            }
        }

        /// <summary>
        /// Method for exporting data to an Excel file 
        /// </summary>
        public static bool GetExcel(List<RepairClass> repairsList, string path)
        {
            // Condition for saving the file 
            if (!string.IsNullOrWhiteSpace(path) && repairsList != null)
            {
                // Create a new Excel workbook 
                var wb = new XLWorkbook();

                // Initialize the interface for adding data to an Excel file 
                var ws = wb.Worksheets.Add("Data");

                // Запись данных в файл Exel
                ws.Cell(1, 1).InsertTable(repairsList);

                // Remove columns with unnecessary information for the user 
                ws.Column("B").Delete();
                ws.Column("A").Delete();

                // Get object properties to retrieve attributes 
                var repairAttributes = repairsList.FirstOrDefault().GetType().GetProperties().ToList();

                repairAttributes.RemoveAll(x => x.GetCustomAttribute<DisplayAttribute>() == null);

                // Loop for getting property and setting column headings 
                for (var i = 0; i < repairAttributes.Count; i++)
                {
                    // Writing the titles of the extracted property attributes to the cells 
                    ws.Cell(1, i + 1).SetValue(repairAttributes[i].GetCustomAttribute<DisplayAttribute>().Name);
                }

                // Set the width of the cell to match the width of the text 
                ws.Columns().AdjustToContents();

                // Disable default auto filter 
                ws.RangeUsed().SetAutoFilter(false);

                try
                {
                    // Save the file 
                    wb.SaveAs(path);

                    // Indicate the result of executing actions with the file upon successful saving 
                    return true;
                }
                catch (Exception ex)
                {
                    // Indicate the result of executing actions with the file if saving error 
                    throw new Exception(ex.Message);
                }
                finally
                {
                    wb.Dispose();
                }
            }
            else
            {
                // Indicate the result of performing actions with the file when canceling the save 
                return false;
            }

        }

        /// <summary>
        /// The method reads the Excel file and loads it into memory for further writing the data to the database 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="emptyCol"></param>
        /// <returns></returns>
        public string LoadExcel(string path, List<RepairClass> emptyCol)
        {
            if (!string.IsNullOrEmpty(path))
            {
                using (MemoryStream ms = new MemoryStream(File.ReadAllBytes(path)))
                {
                    // Create an Excel workbook 
                    using (var workbook = new XLWorkbook(ms))
                    {
                        // Create a table 
                        var worksheet = workbook.Worksheet(1);

                        try
                        {
                            // Forming table rows 
                            var rows = worksheet.RangeUsed().RowsUsed().Skip(1);

                            // Loop for creating objects to add to the collection 
                            foreach (var t in rows)
                            {
                                RepairClass repairClass = new RepairClass()
                                {
                                    Id = 0,
                                    DeviceId = 0,
                                    Date = t.Cell(1).Value.GetType() != typeof(DateTime) ? new DateTime?() : Convert.ToDateTime(t.Cell(1).Value),
                                    Status = t.Cell(2).Value.ToString(),
                                    OsName = t.Cell(3).Value.ToString(),
                                    Model = t.Cell(4).Value.ToString(),
                                    SNumber = t.Cell(5).Value.ToString(),
                                    InvNumber = t.Cell(6).Value.ToString(),
                                    ScOks = t.Cell(7).Value.ToString(),
                                    DiagNumber = t.Cell(8).Value.ToString(),
                                    KaProvider = t.Cell(9).Value.ToString(),
                                    KaRepair = t.Cell(10).Value.ToString(),
                                    HandedOver = t.Cell(11).Value.ToString(),
                                    Defect = t.Cell(12).Value.ToString(),
                                    ShipmentDate = t.Cell(13).Value.GetType() != typeof(DateTime) ? new DateTime?() : Convert.ToDateTime(t.Cell(13).Value),
                                    DaysOfRepair = 0,
                                    ReturnFromRepair = t.Cell(14).Value.GetType() != typeof(DateTime) ? new DateTime?() : Convert.ToDateTime(t.Cell(14).Value),
                                    ProviderOrder = t.Cell(15).Value.ToString(),
                                    RepairBill = t.Cell(16).Value.ToString(),
                                    WarrantyBasis = t.Cell(17).Value.ToString(),
                                    StartWarranty = t.Cell(18).Value.GetType() != typeof(DateTime) ? new DateTime?() : Convert.ToDateTime(t.Cell(18).Value),
                                    Warranty = t.Cell(19).Value.ToString(),
                                    HaveAccumulator = (bool)t.Cell(20).Value,
                                    HaveFlashMemory = (bool)t.Cell(21).Value,
                                    HaveHandBelt = (bool)t.Cell(22).Value,
                                    HaveStylus = (bool)t.Cell(20).Value
                                };

                                emptyCol.Add(repairClass);
                            }

                            return "Файл считан.";
                        }
                        catch (Exception e)
                        {
                            return e.Message;
                        }
                    }
                }
            }
            else
            {
                return null;
            }
        }
    }
}
