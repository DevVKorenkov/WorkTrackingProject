using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net;
using WorkTrackingLib.Models;

namespace Updater
{
    public delegate void MessageDelegate(string msg);
    public delegate void ProgDelegate(int a, int b);

    class UpdateModel
    {
        #region Fields 

        /// <summary>
        /// Program name field 
        /// </summary>
        public const string ProgName = "Work_Tracking.exe";
        /// <summary>
        /// The name of the program to update
        /// </summary>
        public const string UpdatePackage = "WorkTrackingUpdate.zip";
        /// <summary>
        /// Update window message event 
        /// </summary>
        public static event MessageDelegate MesEvent;
        /// <summary>
        /// Event and stop of the progress bar 
        /// </summary>
        public static event MessageDelegate StopProgress;

        static string path = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory).Parent.FullName;

        #endregion

        #region Methods

        /// <summary>
        /// update method
        /// </summary>
        /// <returns></returns>
        public bool UpdateProgramm()
        {
            MesEvent("Обновление");

            try
            {
                if (DownloadFile())
                {
                    if (File.Exists($@"{path}\{UpdatePackage}"))
                    {
                        var tempFiles = Directory.GetFiles(path);

                        try
                        {
                            if (Directory.Exists($@"{path}\dll"))
                            {
                                Directory.Delete($@"{path}\dll", true);
                            }
                        }
                        catch
                        { }

                        foreach (var t in tempFiles)
                        {
                            if (t.Contains("WorkTrackingUpdate.zip"))
                                continue;
                            else
                            {
                                var tempProcess = GetActualProcess();

                                if (tempProcess != null)
                                {
                                    KillProcess(tempProcess);
                                }

                                File.Delete(t);
                            }
                        }

                        ZipFile.ExtractToDirectory($@"{path}\{UpdatePackage}", $@"{path}");
                    }

                    // Get a new version of the program to display 
                    FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo($@"{ProgName}");

                    File.Delete($@"{path}\{UpdatePackage}");

                    StopProgress?.Invoke($@"Обновление завершено. Новая версия программы {fileVersionInfo.FileVersion}");
                }
                else
                {
                    StopProgress?.Invoke("Ошибка скачивания");
                }
               
                return true;
            }
            catch (Exception ex)
            {
                StopProgress?.Invoke($"Ошибка обновления: {ex.InnerException.Message} Программа будет перезапущена через 5 сек.");

                File.Delete($@"{path}\{UpdatePackage}");

                return false;
            }
        }

        private bool DownloadFile()
        {
            // Initialize an instance of the Web client class to download the update
            WebClient webClient = new WebClient();

            try
            {
                var updateServer = JsonConvert.DeserializeObject<ConnectionPathInfo>(File.ReadAllText($@"{path}/Resources/serverInfo.json"));

                // Скачивание новой версии программы
                webClient.DownloadFile(new Uri($@"http://{updateServer.UpdateServer}/Update/{UpdatePackage}"), $@"{path}\{UpdatePackage}");

                return true;
            }
            catch
            {
                return false;
            }
        }

        private Process GetActualProcess()
        {
            Process[] proc = Process.GetProcesses();

            Process temp = null;

            foreach (var t in proc)
            {
                if (t.ProcessName.Contains("Work Tracking"))
                {
                    temp = t;
                    break;
                }
                else
                {
                    temp = null;
                }
            }

            return temp;
        }

        /// <summary>
        /// Method for canceling the current action 
        /// </summary>
        /// <param name="result"></param>
        private void KillProcess(Process killedProcess)
        {
            killedProcess.Kill();
            killedProcess.WaitForExit();
        }

        #endregion
    }
}
