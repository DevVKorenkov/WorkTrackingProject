using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using NewWorkTracking.Actions;
using NewWorkTracking.Connection;
using NewWorkTracking.Extestions;
using NewWorkTracking.Models;
using NewWorkTracking.UpdateApi;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.DirectoryServices.AccountManagement;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WorkTrackingLib;
using WorkTrackingLib.Models;
using WorkTrackingLib.ProcessClasses;

namespace NewWorkTracking.ViewModels
{
    class ProgressBarViewModel : PropertyChangeClass
    {
        /// <summary>
        /// Field of the object for checking for updates 
        /// </summary>
        GetNewVersionApi getNewVersionApi;
        /// <summary>
        /// Server connection field 
        /// </summary>
        ConnectionClass connection;

        private string status;
        /// <summary>
        /// The property of displaying the current action on the screen 
        /// </summary>
        public string Status
        {
            get => status;
            set { status = value; OnPropertyChanged(nameof(Status)); }
        }

        private string newServer;
        /// <summary>
        /// New server field 
        /// </summary>
        public string NewServer
        {
            get => newServer;
            set { newServer = value; OnPropertyChanged(nameof(NewServer)); }
        }

        private bool controlEnable;
        /// <summary>
        /// Field of activation \ deactivation of control 
        /// </summary>
        public bool ControlEnable
        {
            get { return controlEnable; }
            set { controlEnable = value; OnPropertyChanged(nameof(ControlEnable)); }
        }

        private Visibility buttonsVis;
        /// <summary>
        /// The visibility property of the control buttons 
        /// </summary>
        public Visibility ButtonsVis
        {
            get => buttonsVis;
            set { buttonsVis = value; OnPropertyChanged(nameof(ButtonsVis)); }
        }

        private Visibility changeTextBoxVis;
        /// <summary>
        /// Visibility property of the new server input field 
        /// </summary>
        public Visibility ChangeTextBoxVis
        {
            get => changeTextBoxVis;
            set { changeTextBoxVis = value; OnPropertyChanged(nameof(ChangeTextBoxVis)); }
        }

        public ICommand ChouseMenu => new RelayCommand<object>(async obj =>
        {
            switch (obj)
            {
                case "Yes":
                    ChangeTextBoxVis = Visibility.Visible;
                    break;
                case "No":
                    Application.Current.Shutdown();
                    break;
                case "Repeat":
                    ButtonsVis = Visibility.Collapsed;
                    ChangeTextBoxVis = Visibility.Collapsed;
                    await StartApp();
                    break;
            }
        });

        /// <summary>
        /// Command to change server 
        /// </summary>
        public ICommand ChangeServer
        {
            get
            {
                return new RelayCommand<object>(async obg =>
                {
                    Status = "Проверка введенных данных. Подождите.";

                    ControlEnable = false;

                    if (await newServer.WriteNewServer())
                    {
                        WindowUsage.RestartProgramm("ProgrBar");
                    }
                    else
                    {
                        OutputCantConnection($"Ошибка подключения к новому серверу. Проверьте введенные данные, сервер не будет изменен. Введенный вами сервер {NewServer}. Текущий сервер: {ConnectionClass.connectionPath.Server}");

                        ControlEnable = true;
                    }
                });
            }
        }

        public ProgressBarViewModel()
        {
            connection = new ConnectionClass();
            ButtonsVis = Visibility.Collapsed;
            ChangeTextBoxVis = Visibility.Collapsed;
            ControlEnable = true;
            getNewVersionApi = new GetNewVersionApi();

            Task.Run(() =>
            {
                Status = "Проверка обновлений.";
                if (connection.CreateConnectionString())
                {
                    StartCheckUpdate();
                }
                else
                {
                    OutputCantConnection($@"Подключение отсутствует. Проверьте сетевое подключение и адрес сервера. Повторить попытку подключения или изменить сервер? ");
                }
            });
        }

        /// <summary>
        /// Connection start
        /// </summary>
        /// <returns></returns>
        private async Task<bool> StartConnection()
        {
            try
            {
                Status = "Проверка подключения.";

                await ConnectionClass.hubConnection.StartAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Creates and tests a connection 
        /// </summary>
        /// <returns></returns>
        private async Task StartApp()
        {
            if (await StartConnection())
            {
                try
                {
                    StartCheckAccess();

                    StartProgram();
                }
                catch (Exception e)
                {
                    OutputCantConnection(e.Message);
                }
            }
            else
            {
                OutputCantConnection($@"Подключение отсутствует. Проверьте сетевое подключение и адрес сервера. Повторить попытку подключения или изменить сервер? Текущий сервер: ""{ConnectionClass.connectionPath.Server}""");
            }
        }

        /// <summary>
        /// Checks the access level
        /// </summary>
        private void StartCheckAccess()
        {
            Status = "Подключение установлено.";

            var userName = UserPrincipal.Current.DisplayName;

            ConnectionClass.hubConnection.InvokeAsync("RunCheckAccess", userName);

            ConnectionClass.hubConnection.On<bool>("AccessDenide", (access) => OutputCantConnection("Доступ запрещен. Обратитесь к руководителю или в отдел технической поддержки"));

            return;
        }

        /// <summary>
        /// Launches the client application
        /// </summary>
        private void StartProgram()
        {
            Status = "Получение данных.";

            ConnectionClass.hubConnection.On<MainObject>("GiveAll", (mainObject) =>
            {
                mainObject.ComboBox.Accesses = mainObject.ComboBox.Accesses.OrderBy(x => x.Name).ToList();

                Status = "Запуск программы.";

                Application.Current.Dispatcher.Invoke(() => StartMainWindow(mainObject));
            });
        }

        /// <summary>
        /// Displaying a connection error message
        /// </summary>
        /// <param name="outMessage"></param>
        private void OutputCantConnection(string outMessage)
        {
            if (File.Exists(@"Resources/serverInfo.json"))
            {
                Status = outMessage;

                ButtonsVis = Visibility.Visible;
            }
            else
            {
                if (CreateConnectionFile())
                {
                    Status = "Отсутствует файл подключения к серверу. Создан стандартный файл подключения. Перезапустите программу и укажите корректный сервер.";
                }
                else
                {
                    Status = "Отсутствует файл подключения к серверу. Создать стандартный файл подключения не удалось. Переустановите программу.";
                }
            }
        }

        private async void StartCheckUpdate()
        {
            if (!getNewVersionApi.CheckUpdate(Convert.ToInt32(FileVersionInfo.GetVersionInfo($@"{Directory.GetCurrentDirectory()}/Work_Tracking.exe").FileVersion.Replace(".", ""))))
                await StartApp();
            else
            {
                if (Application.Current.Dispatcher.Invoke(() => Message.Show("Внимание", "Обноружено обновление. Установить?", MessageBoxButton.YesNo) == MessageBoxResult.Yes))
                    StartUpdate();
                else
                    await StartApp();
            }
        }

        /// <summary>
        /// Launching the main window
        /// </summary>
        /// <param name="mainObject"></param>
        private void StartMainWindow(MainObject mainObject)
        {
            MainWindowViewModel mainWindowViewModel = new MainWindowViewModel(mainObject);

            MainWindow mainWindow = new MainWindow()
            {
                DataContext = mainWindowViewModel
            };

            mainWindow.Show();

            WindowUsage.CloseWindow("ProgrBar");
        }

        /// <summary>
        /// Launching the update
        /// </summary>
        private void StartUpdate()
        {
            if (File.Exists(@"Updater\Updater.exe"))
            {
                Process.Start(@"Updater\Updater.exe");

                Process.GetCurrentProcess().Kill();
            }
            else
            {
                Message.Show("Ошибка", "Отсутствует модуль обновления. Рекомендуется переустановка программы", MessageBoxButton.OK);

                StartApp();
            }
        }

        /// <summary>
        /// Creates a missing connection file
        /// </summary>
        /// <returns></returns>
        private bool CreateConnectionFile()
        {
            try
            {
                if (Directory.Exists("Resources"))
                {
                    ConnectionPathInfo connectionPath = new ConnectionPathInfo() { Server = "#", UpdateServer = "" };

                    File.WriteAllText(@"Resources/serverInfo.json", JsonConvert.SerializeObject(connectionPath));

                    return true;
                }
                else
                {
                    Directory.CreateDirectory("Resources");

                    CreateConnectionFile();

                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
