using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WorkTrackingLib.Models;

namespace NewWorkTracking.Connection
{
    /// <summary>
    /// Contains the logic for getting the connection string and activating the connection 
    /// </summary>
    class ConnectionClass
    {
        /// <summary>
        /// Main connection object 
        /// </summary>
        public static ConnectionPathInfo connectionPath;

        /// <summary>
        /// Connection string 
        /// </summary>
        public static HubConnection hubConnection;

        /// <summary>
        /// Method for creating connection string 
        /// </summary>
        /// <returns></returns>
        public bool CreateConnectionString()
        {
            try
            {
                // Deserialize the connection file 
                connectionPath = JsonConvert.DeserializeObject<ConnectionPathInfo>(File.ReadAllText(@"Resources/serverInfo.json"));

                // Composing the connection string 
                hubConnection = new HubConnectionBuilder().WithUrl($"http://{connectionPath.Server}:5010/TrackingServer").Build();

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
