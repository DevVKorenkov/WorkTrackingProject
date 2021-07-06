using NewWorkTracking.Connection;
using NewWorkTracking.Extestions;
using NewWorkTracking.Models;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WorkTrackingLib;
using WorkTrackingLib.ProcessClasses;

namespace NewWorkTracking.ViewModels
{
    class ChangeServerViewModel : PropertyChangeClass
    {
        private string description;
        /// <summary>
        /// Output property of the current action
        /// </summary>
        public string Description
        {
            get => description;
            set { description = value; OnPropertyChanged(nameof(Description)); }
        }

        private string newServer;
        /// <summary>
        /// Property of the new server 
        /// </summary>
        public string NewServer
        {
            get => newServer;
            set { newServer = value; OnPropertyChanged(nameof(NewServer)); }
        }

        private string newUpdateServer;
        /// <summary>
        /// Property of the new server 
        /// </summary>
        public string NewUpdateServer
        {
            get => newUpdateServer;
            set { newUpdateServer = value; OnPropertyChanged(nameof(NewUpdateServer)); }
        }

        private float restartValue;
        public float RestartValue
        {
            get => restartValue;
            set { restartValue = value; OnPropertyChanged(nameof(RestartValue)); }
        }

        private bool controlEnable;
        /// <summary>
        /// Activation \ deactivation of the loading window control
        /// </summary>
        public bool ControlEnable
        {
            get { return controlEnable; }
            set { controlEnable = value; OnPropertyChanged(nameof(ControlEnable)); }
        }

        /// <summary>
        /// Command to change server 
        /// </summary>
        public ICommand ChangeServer => new RelayCommand<object>(async obg =>
        {
            Description = "Проверка введенных данных. Подождите.";

            ControlEnable = false;

            if (!string.IsNullOrWhiteSpace(NewUpdateServer))
            {
                Description = await NewUpdateServer.WriteNewUpdateServer() == true ? $@"Изменения применены. Текущий сервер: ""{ConnectionClass.connectionPath.Server}"", Текущий сервер обновлений: {NewUpdateServer}" :
                $@"Ошибка подключения к новому серверу. Текущий сервер: ""{ConnectionClass.connectionPath.Server}"", текущий сервер обновлений: ""{ConnectionClass.connectionPath.UpdateServer}""";
            }

            if (!string.IsNullOrWhiteSpace(NewServer))
            {
                var result = await newServer.WriteNewServer();

                Description = result == true ? $@"Изменения применены. Текущий сервер: ""{ConnectionClass.connectionPath.Server}"", Текущий сервер обновлений: {ConnectionClass.connectionPath.UpdateServer}." :
                $@"Ошибка подключения к новому серверу. Текущий сервер: ""{ConnectionClass.connectionPath.Server}"", текущий сервер обновлений: ""{ConnectionClass.connectionPath.UpdateServer}""";

                if (result)
                {
                    await Task.Run(() => RestartTimer(5));

                    Process.Start($"Work_Tracking.exe");

                    Application.Current.Shutdown();
                }
            }

            ControlEnable = true;
        }, canExe => !string.IsNullOrWhiteSpace(NewUpdateServer) || !string.IsNullOrWhiteSpace(NewServer));


        public ICommand CloseChangeServer => new RelayCommand<object>(obj =>
        {
            Application.Current.Windows.CloseWindow("ChangeServer");
        });

        public ChangeServerViewModel()
        {
            ControlEnable = true;

            Description = $@"Введите имя или IP-адрес сервера без ""\\"". Пример имени сервера: m1-dl-server. Пример IP-адреса: 10.130.10.10. Текущий сервер: " +
                $@"""{ConnectionClass.connectionPath.Server}"", текущий сервер обновлений: ""{ConnectionClass.connectionPath.UpdateServer}""";
        }

        private void RestartTimer(float maxValue)
        {
            float i = 0;

            while (i < maxValue)
            {
                RestartValue = i;

                i += 0.01f;

                Thread.Sleep(10);
            }
        }
    }
}
