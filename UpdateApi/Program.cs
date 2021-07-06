using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace UpdateApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureKestrel(options =>
                    {
                        #region Прослушивание IP адресов
                        //options.ListenLocalhost(5000);
                        //options.Listen(IPAddress.Parse("10.130.37.28"), 5000);

                        //IPAddress[] iPAddresses = Dns.GetHostAddresses(Environment.MachineName);
                        //foreach (var ipAddress in iPAddresses)
                        //{
                        //    // Адреса типа v4 добавить в прослушку сервера.
                        //    if (ipAddress.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                        //    {
                        //        options.Listen(ipAddress, 5000);
                        //    }
                        //}
                        #endregion

                        options.ListenAnyIP(5011);
                    })
                   .UseStartup<Startup>();
                }).UseWindowsService();
    }
}
