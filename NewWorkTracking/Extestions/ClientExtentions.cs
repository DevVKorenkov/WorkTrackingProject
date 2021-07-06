using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using NewWorkTracking.Connection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WorkTrackingLib;
using WorkTrackingLib.Models;

namespace NewWorkTracking.Extestions
{
    static class ClientExtentions
    {
        static HubConnection hubConnection;

        static HttpClient client;

        /// <summary>
        /// (Extension) The method writes data on the new server to the server address file
        /// </summary>
        /// <param name="newServer"></param>
        public async static Task<bool> WriteNewServer(this string newServer)
        {
            hubConnection = CreateTempConnectionString(newServer);

            if (hubConnection != null)
            {
                // Check the connection to the new server 
                if (await hubConnection.CheckNewServer())
                {
                    try
                    {
                        // Deserialize json connection file 
                        var tempReadedFile = JsonConvert.DeserializeObject<ConnectionPathInfo>(File.ReadAllText(@"Resources/serverInfo.json"));

                        // Temporary variable of the connection object 
                        tempReadedFile.Server = newServer;

                        // Write temporary object to Json file 
                        File.WriteAllText(@"Resources/serverInfo.json", JsonConvert.SerializeObject(tempReadedFile));

                        // Deserialize the actual connection file into the main connection object 
                        ConnectionClass.connectionPath = JsonConvert.DeserializeObject<ConnectionPathInfo>(File.ReadAllText(@"Resources/serverInfo.json"));

                        await hubConnection.DisposeAsync();

                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                }
                else
                    return false;
            }
            else
                return false;

        }

        /// <summary>
        /// (Extension) The method writes data on the new update server to the server address file
        /// </summary>
        /// <param name="newUpdateServer"></param>
        /// <returns></returns>
        public async static Task<bool> WriteNewUpdateServer(this string newUpdateServer)
        {
            client = new HttpClient();

            // Deserialize json connection file 
            var tempReadedFile = JsonConvert.DeserializeObject<ConnectionPathInfo>(File.ReadAllText(@"Resources/serverInfo.json"));

            tempReadedFile.UpdateServer = newUpdateServer.Replace(",", ".");

            try
            {
                var test = await client.GetStringAsync($"http://{tempReadedFile.UpdateServer}/api/Update/Check");

                // Write temporary object to Json file 
                File.WriteAllText(@"Resources/serverInfo.json", JsonConvert.SerializeObject(tempReadedFile));

                return true;
            }
            catch
            {
                return false;
            }
        }

        private static HubConnection CreateTempConnectionString(string newServer)
        {
            try
            {
                // Создание тестовой строки подключения
                return hubConnection = new HubConnectionBuilder().WithUrl($"http://{newServer = newServer.Replace(",", ".")}:5010/TrackingServer").Build();
            }
            catch (Exception ex)
            {
                //Message.Show("Ошибка", ex.Message, MessageBoxButton.OK);

                return null;
            }
        }

        /// <summary>
        /// (Расширение) Метод проверки корректности введенных данных
        /// </summary>
        /// <param name="testCon"></param>
        /// <returns></returns>
        public async static Task<bool> CheckNewServer(this HubConnection testCon)
        {
            try
            {
                await testCon.StartAsync();

                await testCon.InvokeAsync("TestConnection");

                await testCon.StopAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// (Расширение) Метод определяет открыто ли окно с определенным именем в данный момент
        /// </summary>
        /// <param name="windows"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool ContainsWindow(this WindowCollection windows, string name)
        {
            bool temp = false;

            foreach (Window t in windows)
            {
                if (t.Name == name)
                {
                    temp = true;
                }
            }

            return temp;
        }

        /// <summary>
        /// (Расширение) Метод закрытия окна
        /// </summary>
        /// <param name="windows"></param>
        /// <param name="name"></param>
        public static void CloseWindow(this WindowCollection windows, string name)
        {
            foreach (Window t in windows)
            {
                if (t.Name == name)
                {
                    t.Close();
                }
            }
        }

        public static INotifyCollectionChanged SelectionChanged(this INotifyCollectionChanged selectedItems, IList list)
        {
            ObservableCollection<object> tempCol = new ObservableCollection<object>();

            foreach (var i in list)
            {
                if (i is NewWrite WriteItem)
                    tempCol.Add(WriteItem);
                else if (i is RepairCard RepairItem)
                    tempCol.Add(RepairItem);
            }

            return tempCol;
        }
    }
}
