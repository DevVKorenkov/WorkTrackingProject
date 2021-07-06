using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NewWorkTracking.Actions
{
    /// <summary>
    /// Contains the logic for working with windows
    /// </summary>
    class WindowUsage
    {
        /// <summary>
        /// Method for restarting the program        /// </summary>
        public static void RestartProgramm(string WindowName)
        {
            // Close the ProgBar window 
            foreach (Window t in Application.Current.Windows)
            {
                if (t.Name.ToString() == $"{WindowName}")
                {
                    t.Close();
                }
            }

            System.Windows.Forms.Application.Restart();
        }

        /// <summary>
        /// Method for closing windows 
        /// </summary>
        /// <param name="WindowName"></param>
        public static void CloseWindow(string WindowName)
        {
            // Закрытие окна ProgBar
            foreach (Window t in Application.Current.Windows)
            {
                if (t.Name.ToString() == $"{WindowName}")
                {
                    t.Close();
                }
            }
        }
    }
}
