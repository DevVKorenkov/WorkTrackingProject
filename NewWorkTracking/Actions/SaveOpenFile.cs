using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewWorkTracking.Actions
{
    /// <summary>
    /// Contains the logic for launching the file selection dialog box
    /// </summary>
    class SaveOpenFile
    {
        /// <summary>
        /// Method for calling the file save diolog 
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public string SaveDialog(string fileName)
        {
            /// Method for calling the file save dialog 
            SaveFileDialog sfd = new SaveFileDialog();

            // Filter file extensions of the save file dialog 
            sfd.Filter = "Файл Excel 2007+ (*.xlsx)|*.xlsx|Файл Exel 2003 (*.xls)|*.xls";

            sfd.FileName = fileName;

            // Launch the save file dialog 
            if (sfd.ShowDialog() == true)
            {
                return sfd.FileName;
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// The method opens a file selection dialog box and returns the full path to the file 
        /// </summary>
        /// <returns></returns>
        public string OpenDialog()
        {
            // Initialize the file selection class 
            OpenFileDialog opn = new OpenFileDialog();

            // Filter file extensions 
            opn.Filter = "Файл Excel 2007+ (*.xlsx)|*.xlsx|Файл Excel 2003 (*.xls)|*.xls";

            // condition for normal processing of the dialog box 
            if (opn.ShowDialog() == true)
            {
                return opn.FileName;
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
