using Microsoft.AspNetCore.SignalR.Client;
using NewWorkTracking.Connection;
using NewWorkTracking.Models;
using NewWorkTracking.Windows;
using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using WorkTrackingLib;
using WorkTrackingLib.Models;
using WorkTrackingLib.ProcessClasses;

namespace NewWorkTracking.ViewModels
{
    class MainWindowViewModel : AbstractViewModel
    {
        private UserWorksViewModel userWorksViewModel;
        /// <summary>
        /// Property of the data context of the user's work tab
        /// </summary>
        public UserWorksViewModel UserWorksViewModel
        {
            get => userWorksViewModel;
            set { userWorksViewModel = value; OnPropertyChanged(nameof(UserWorksViewModel)); }
        }

        private AllWorksViewModel allWorksViewModel;
        /// <summary>
        /// Property of the data context of the tab of all works 
        /// </summary>
        public AllWorksViewModel AllWorksViewModel
        {
            get => allWorksViewModel;
            set { allWorksViewModel = value; OnPropertyChanged(nameof(AllWorksViewModel)); }
        }

        private RepairsViewModel repairsViewModel;
        /// <summary>
        /// Property of the data context of the tab of all repairs 
        /// </summary>
        public RepairsViewModel RepairsViewModel
        {
            get => repairsViewModel;
            set { repairsViewModel = value; OnPropertyChanged(nameof(RepairsViewModel)); }
        }

        private DevicesViewModel devicesViewModel;
        /// <summary>
        /// Property of the data context of the device tab 
        /// </summary>
        public DevicesViewModel DevicesViewModel
        {
            get => devicesViewModel;
            set { devicesViewModel = value; OnPropertyChanged(nameof(DevicesViewModel)); }
        }

        private AdministrateViewModel administrateViewModel;
        /// <summary>
        /// Property of the data context of the administration tab 
        /// </summary>
        public AdministrateViewModel AdministrateViewModel
        {
            get => administrateViewModel;
            set { administrateViewModel = value; OnPropertyChanged(nameof(AdministrateViewModel)); }
        }

        private string previewTab;

        private string currentTab;

        private string connectStatus;
        /// <summary>
        /// Property of representing the status of connection to the server 
        /// </summary>
        public string ConnectStatus
        {
            get { return connectStatus; }
            set { connectStatus = value; OnPropertyChanged(nameof(ConnectStatus)); }
        }
       
        private bool stat;
        /// <summary>
        /// Server connection status property
        /// </summary>
        public bool Stat
        {
            get { return stat; }
            set { stat = value; OnPropertyChanged(nameof(Stat)); }
        }

        /// <summary>
        /// Command to change the display of tabs 
        /// </summary>
        public ICommand ViewModelsVisibility => new RelayCommand<object>(obj =>
        {
            SwitchTab(obj.ToString());      
        });

        /// <summary>
        /// Back button command (Not used at the moment) 
        /// </summary>
        public ICommand Back => new RelayCommand<object>(obj =>
        {
            SwitchTab(previewTab);
        });

        /// <summary>
        /// Command to start the server change window 
        /// </summary>
        public ICommand ChangeServer => new RelayCommand<object>(obj => 
        {
            new ChangeServerWindow().ShowDialog();
        });

        public MainWindowViewModel(MainObject mainObject)
        {
            SignalRActions();

            MainObject.Access = mainObject.Access;

            UserWorksViewModel = new UserWorksViewModel(mainObject);

            AllWorksViewModel = new AllWorksViewModel(mainObject);

            RepairsViewModel = new RepairsViewModel(mainObject);

            DevicesViewModel = new DevicesViewModel(mainObject);

            AdministrateViewModel = new AdministrateViewModel(mainObject);

            ConnectionClass.hubConnection.Closed += Reconnect;

            ConnectStatus = $@"Установлено соединение с сервером ""{ConnectionClass.connectionPath.Server}"".";

            Stat = true;
        }

        public MainWindowViewModel()
        {
        }

        /// <summary>
        /// Method for subscribing to server messages
        /// </summary>
        protected override void SignalRActions()
        {
            ConnectionClass.hubConnection.On<AccessModel>("ChangeAccess", (accessModel) =>
            {
                if (accessModel.Name == UserPrincipal.Current.DisplayName)
                {
                    string tempAccessDesc = string.Empty;

                    MainObject.Access = accessModel;

                    switch (MainObject.Access.Access)
                    {
                        case 0:
                            tempAccessDesc = "Просмотр";
                            break;
                        case 1:
                            tempAccessDesc = "Управление";
                            break;
                        case 2:
                            tempAccessDesc = "Администрирование";
                            break;
                    }

                    SwitchTab("User");

                    Application.Current.Dispatcher.Invoke(() => Message.Show("Внимание", $"Ваш уровень доступа изменился на {tempAccessDesc}", MessageBoxButton.OK));
                }
            });
        }

        /// <summary>
        /// Handler for the disconnection event 
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        private Task Reconnect(Exception ex)
        {
            return Task.Run(async () =>
            {
                var i = 1;

                Stat = false;

                while (!Stat)
                {
                    try
                    {
                        ConnectStatus = $"Соединение разорвано. Попытка подключения {i++}";

                        await ConnectionClass.hubConnection.StartAsync();

                        Stat = true;

                        Thread.Sleep(1000);

                        ConnectStatus = $@"Установлено соединение с сервером ""{ConnectionClass.connectionPath.Server}""";
                    }
                    catch { continue; }
                }
            });
        }

        /// <summary>
        /// Method for changing the display of tabs
        /// </summary>
        /// <param name="obj"></param>
        private void SwitchTab(string tabName)
        {
            List<AbstractViewModel> viewModels = new List<AbstractViewModel>() { UserWorksViewModel, AllWorksViewModel, RepairsViewModel, DevicesViewModel, AdministrateViewModel };

            PreviewTab(viewModels);

            foreach (var v in viewModels)
            {
                v.ActiveVisibility = v.GetType().FullName.Contains(tabName) ? Visibility.Visible : Visibility.Collapsed;

                currentTab = v.GetType().FullName;
            }
        }

        private void PreviewTab(List<AbstractViewModel> viewModels)
        {
            foreach (var v in viewModels)
            {
                if (v.ActiveVisibility == Visibility.Visible)
                {
                    previewTab = v.GetType().FullName;
                }
            }
        }
    }
}
