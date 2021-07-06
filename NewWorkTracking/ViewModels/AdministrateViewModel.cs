using Microsoft.AspNetCore.SignalR.Client;
using NewWorkTracking.Actions;
using NewWorkTracking.Connection;
using NewWorkTracking.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using WorkTrackingLib.Interfaces;
using WorkTrackingLib.Models;
using WorkTrackingLib.ProcessClasses;

namespace NewWorkTracking.ViewModels
{
    class AdministrateViewModel : AbstractViewModel
    {
        private List<ISelectedItem> items;
        /// <summary>
        /// Property collection of selected objects 
        /// </summary>
        public List<ISelectedItem> Items
        {
            get => items;
            set { items = value; OnPropertyChanged(nameof(Items)); }
        }

        private string catSelected;
        /// <summary>
        /// Property of the selected category
        /// </summary>
        public string CatSelected
        {
            get => catSelected;
            set
            {
                catSelected = value;
                OnPropertyChanged(CatSelected);
                ChangeCat();
            }
        }

        /// <summary>
        /// Table field 
        /// </summary>
        private string table;

        /// <summary>
        /// Field of the copy of the selected object 
        /// </summary>
        private ISelectedItem selectedItemCopy;

        private ISelectedItem selectedItem;
        /// <summary>
        /// Property of the selected object 
        /// </summary>
        public ISelectedItem SelectedItem
        {
            get => selectedItem;
            set { selectedItem = value; OnPropertyChanged(nameof(SelectedItem)); if (SelectedItem != null) selectedItemCopy = (ISelectedItem)SelectedItem.Clone(); }
        }

        private List<int> accessLevels;
        /// <summary>
        /// Property list of access levels
        /// </summary>
        public List<int> AccessLevels
        {
            get => accessLevels;
            set { accessLevels = value; OnPropertyChanged(nameof(AccessLevels)); }
        }

        /// <summary>
        /// A copy of the selected user object 
        /// </summary>
        private Admins selectedAdminCopy;

        private Admins selectedAdmin;
        /// <summary>
        /// Property of the selected user 
        /// </summary>
        public Admins SelectedAdmin
        {
            get => selectedAdmin;
            set
            {
                selectedAdmin = value;
                if (SelectedAdmin != null)
                    selectedAdminCopy = (Admins)SelectedAdmin.Clone();
                OnPropertyChanged(nameof(SelectedAdmin));
            }
        }

        private int accessLevel;
        /// <summary>
        /// Access level property 
        /// </summary>
        public int AccessLevel
        {
            get => accessLevel;
            set { accessLevel = value; OnPropertyChanged(nameof(AccessLevel)); }
        }

        private string selectedScOks;
        /// <summary>
        /// Access level property 
        /// </summary>
        public string SelectedScOks
        {
            get => selectedScOks;
            set { selectedScOks = value; OnPropertyChanged(nameof(SelectedScOks)); }
        }

        private string newUser;
        /// <summary>
        /// New username property 
        /// </summary>
        public string NewUser
        {
            get => newUser;
            set { newUser = value; OnPropertyChanged(nameof(NewUser)); }
        }

        private string item;
        /// <summary>
        /// Property of the new object 
        /// </summary>
        public string Item
        {
            get => item;
            set { item = value; OnPropertyChanged(nameof(Item)); }
        }

        private ObservableCollection<string> userList;
        public ObservableCollection<string> UserList
        {
            get => userList;
            set { userList = value; OnPropertyChanged(nameof(UserList)); }
        }

        private Dispatcher dispatcher;

        public ICommand SearchName => new RelayCommand<object>(obj =>
        {
            GetUsersList();
        });

        /// <summary>
        /// Command for adding a new object 
        /// </summary>
        public ICommand AddObject => new RelayCommand<object>(obj =>
        {
            // Action if a user is added
            if ((string)obj == "User")
            {
                // Confirmation of action
                if (Message.Show("Внимание", $@"Добавить нового пользователя с Ф.И.О ""{NewUser}"" и уровнем доступа ""{AccessLevel}""?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    // Send a request and a new user object to the server
                    ConnectionClass.hubConnection.InvokeAsync("RunAddNewUser", new Admins() { Name = NewUser, Access = AccessLevel, ScOKS = SelectedScOks });

                    // Clear the new user string
                    NewUser = string.Empty;
                }
            }
            // action if added object other than user object 
            else
            {
                // Confirmation of action
                if (Message.Show("Внимание", $@"Добавить ""{Item}""?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    // Отправка запроса и объекта нового объекта на сервер
                    ConnectionClass.hubConnection.InvokeAsync("RunAddNewItem", new BaseTableModel() { Name = Item }, table);

                    // Clear the line of the new object
                    Item = string.Empty;

                    ChangeCat();
                }
            }
        });

        /// <summary>
        /// Command to change the selected object
        /// </summary>
        public ICommand ChangeItem => new RelayCommand<object>(obj =>
        {
            // Action on user change 
            if ((string)obj == "User")
            {
                // Confirmation of action 
                if (Message.Show("Внимание", $@"Изменить пользователя ""{selectedAdminCopy.Name}"" с уровнем доступа ""{selectedAdminCopy.Access}"" на ""{SelectedAdmin.Name}"" и уровнем доступа ""{SelectedAdmin.Access}""?",
                    MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    // Send the request and modified user object to the server 
                    //var admin = new Admins() { Id = SelectedAdmin.Id, Name = SelectedAdmin.Name, Access = SelectedAdmin.Access, ScOKS = SelectedAdmin.ScOKS };
                    ConnectionClass.hubConnection.InvokeAsync("StartChangeUser", SelectedAdmin);
                }
                else
                {
                    SelectedAdmin = selectedAdminCopy;
                }
            }
            // Action when the object changes 
            else
            {
                // Confirmation of action
                if (Message.Show("Внимание", $@"Изменить ""{selectedItemCopy.Name}"" на ""{SelectedItem.Name}""?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    // Send the request and the changed object to the server 
                    ConnectionClass.hubConnection.InvokeAsync("StartChangeItem", SelectedItem, table);
                }
                else
                {
                    SelectedItem = selectedItemCopy;
                }
            }
        });

        /// <summary>
        /// Command to delete the selected object
        /// </summary>
        public ICommand DelObject => new RelayCommand<object>(obj =>
        {
            // Action when deleting a user
            if ((string)obj == "User")
            {
                if (Message.Show("Внимание", $@"Удалить пользователя ""{SelectedAdmin.Name}"" с уровнем доступа ""{SelectedAdmin.Access}""?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    // Send a request to delete a user to the server 
                    ConnectionClass.hubConnection.InvokeAsync("RunDelObject", SelectedAdmin, "User");

                    SelectedAdmin = null;
                }
            }
            // Action when deleting an object 
            else
            {
                if (Message.Show("Внимание", $@"Удалить ""{SelectedItem.Name}""?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    // Sending a request to delete an object to the server 
                    ConnectionClass.hubConnection.InvokeAsync("RunDelObject", SelectedItem, table);

                    SelectedItem = null;
                }
            }
        });

        public AdministrateViewModel(MainObject mainObject)
        {
            dispatcher = Application.Current.Dispatcher;
            NewDictionary();
            MainObject = mainObject;
            SignalRActions();
            CatSelected = "СцОкс";
            UserList = new ObservableCollection<string>();
        }

        /// <summary>
        /// Method of subscribing to messages from the server 
        /// </summary>
        protected override void SignalRActions()
        {
            // Action on user change 
            ConnectionClass.hubConnection.On<Admins>("UpdateUsers", (user) =>
            {
                dispatcher.Invoke(() =>
                {
                    MainObject.ComboBox.Accesses.Add(user);

                    MainObject.ComboBox.Accesses = new List<Admins>(MainObject.ComboBox.Accesses.OrderBy(x => x.Name));
                });
            });

            // Action when the object changes 
            ConnectionClass.hubConnection.On<ComboboxDataSource>("UpdateItem", (comboboxes) =>
            {
                comboboxes.Accesses = comboboxes.Accesses.OrderBy(x => x.Name).ToList();

                dispatcher.Invoke(() => MainObject.ComboBox = comboboxes); ChangeCat();
            });
        }

        private void NewDictionary()
        {
            AccessLevels = new List<int>()
            {
                0,
                1,
                2,
            };
        }

        /// <summary>
        /// Method for changing the name of tables
        /// </summary>
        private void ChangeCat()
        {
            if (!string.IsNullOrEmpty(CatSelected))
            {
                switch (CatSelected.Remove(0, CatSelected.IndexOf(' ') + 1))
                {
                    case "СцОкс":
                        Items = new List<ISelectedItem>(MainObject.ComboBox.ScOks);
                        table = "ScOks";
                        break;
                    case "ОСП":
                        Task.Run(() => Items = new List<ISelectedItem>(MainObject.ComboBox.OspList));
                        table = "Osp";
                        break;
                    case "Типы ОС":
                        Items = new List<ISelectedItem>(MainObject.ComboBox.OsTypeList);
                        table = "OsType";
                        break;
                    case "Результаты работ":
                        Items = new List<ISelectedItem>(MainObject.ComboBox.ResultsList);
                        table = "Results";
                        break;
                    case "Причины обращения":
                        Items = new List<ISelectedItem>(MainObject.ComboBox.WhyList);
                        table = "Why";
                        break;
                    case "Статусы ремонта":
                        Items = new List<ISelectedItem>(MainObject.ComboBox.RepairStatus);
                        table = "RepairStatus";
                        break;
                }
            }
        }

        private async void GetUsersList()
        {
            var ad = new AdUsage();

            ad.UserSearchEvent += Ad_TestEvent;

            await Task.Run(() =>
            {
                try
                {
                    UserList = ad.GetAdUsers("User", NewUser);
                }
                catch (Exception e)
                {
                    if (e.GetType() == typeof(ArgumentException))
                        NewUser = "Заполните поле поиска.";
                }
            });

            ad.UserSearchEvent -= Ad_TestEvent;
        }

        private void Ad_TestEvent(string m)
        {
            NewUser = $"{m}";
        }
    }
}
