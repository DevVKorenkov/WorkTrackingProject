using Newtonsoft.Json;
using NewWorkTracking.Connection;
using NewWorkTracking.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WorkTrackingLib.Interfaces;
using WorkTrackingLib.Models;

namespace NewWorkTracking.UpdateApi
{
    /// <summary>
    /// Contains the logic for updating the program 
    /// </summary>
    public class GetNewVersionApi : INewVersionApi
    {
        private HttpClient client;

        public GetNewVersionApi()
        {
            client = new HttpClient();
        }

        /// <summary>
        /// Method for checking program updates 
        /// </summary>
        /// <param name="version"></param>
        /// <returns></returns>
        public bool CheckUpdate(int version)
        {
            try
            {
                // Formation of the connection string to the update server
                string url = $@"http://{ConnectionClass.connectionPath.UpdateServer}/api/Update/Check";

                // Get the program version from the update server 
                var updateVersion = JsonConvert.DeserializeObject<ProgramInfo>(client.GetStringAsync(url).Result);

                // Сравнение версий
                if (updateVersion != null && updateVersion.Version > version)
                {
                    return true;
                }
                // Action on failure to retrieve data from the version file 
                else if (updateVersion == null)
                {
                    Application.Current.Dispatcher.Invoke(() => Message.Show("Внимание", "Не удалось получить файл версии", MessageBoxButton.OK));

                    return false;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                Application.Current.Dispatcher.Invoke(() => Message.Show("Внимание", "Нет подключения к серверу обновлений", MessageBoxButton.OK));

                return false;
            }
        }
    }
}
