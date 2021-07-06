using Microsoft.AspNetCore.SignalR.Client;
using NewWorkTracking.Actions;
using NewWorkTracking.Connection;
using NewWorkTracking.Filters;
using NewWorkTracking.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Threading;
using WorkTrackingLib;
using WorkTrackingLib.Models;
using WorkTrackingLib.ProcessClasses;

namespace NewWorkTracking.ViewModels
{
    class AllWorksViewModel : AbstractViewModel
    {
        private Dispatcher dispatcher;

        /// <summary>
        /// Property List of statuses of the work object 
        /// </summary>
        public ObservableCollection<string> OrderActivity { get; set; } =
            new ObservableCollection<string>() { "Активно", "Архив", "Необходимо заполнить номер модернизации или списания", "Списано", "Модернизировано" };

        /// <summary>
        /// The List of selected objects in the DataGrid        
        /// </summary>
        private static ObservableCollection<NewWrite> selectedOrders;

        private NewWrite selectedOrder;
        /// <summary>
        /// Property of the selected object
        /// </summary>
        public NewWrite SelectedOrder
        {
            get => selectedOrder;
            set
            {
                selectedOrder = value; OnPropertyChanged(nameof(SelectedOrder));

                if (SelectedOrder != null)
                    selectedOrderCopy = (NewWrite)SelectedOrder.Clone();
            }
        }

        /// <summary>
        /// Field of the copy of the selected object 
        /// </summary>
        private NewWrite selectedOrderCopy;

        //private WorkCardDataContext workContext;
        //public WorkCardDataContext WorkContext
        //{
        //    get => workContext;
        //    set { workContext = value; OnPropertyChanged(nameof(WorkContext)); }
        //}

        private string status;
        /// <summary>
        /// Order status field 
        /// </summary>
        public string Status
        {
            get => status;
            set { status = value; OnPropertyChanged(nameof(Status)); }
        }

        /// <summary>
        /// Command to upload an Excel file 
        /// </summary>
        public ICommand UploadExcel => new RelayCommand<object>(obj =>
        {
            GetExele();
        });

        /// <summary>
        /// Command to remove an object from the report 
        /// </summary>
        public ICommand RemoveFromReport
        {
            get
            {
                return new RelayCommand<object>(obj =>
                {
                    var temp = selectedOrders.Count;

                    if (Message.Show("Внимание", $"Удалить {temp} шт. объектов? Удаление произойдет только из отчета, в базе данных изменений не произойдет", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        foreach (var t in selectedOrders)
                        {
                            UsersWorks.Remove(t);
                        }
                    }
                });
            }
        }

        /// <summary>
        /// command for changing data in the selected object 
        /// </summary>
        public ICommand ChangeWork => new RelayCommand<object>(obj =>
        {
            // Send the request and the changed object to the server 
            ConnectionClass.hubConnection.InvokeAsync("StartChangeWork", SelectedOrder);
        });

        /// <summary>
        /// Clean filters 
        /// </summary>
        public ICommand RefreshFilters => new RelayCommand<object>(obj =>
        {
            // Loop for getting and removing property values 
            foreach (var p in Filter.GetType().GetProperties())
            {
                if (p.PropertyType == typeof(DateTime?))
                {
                    p.SetValue(Filter, null);
                }
                else if (p.PropertyType == typeof(bool))
                {
                    p.SetValue(Filter, false);
                }
                else
                {
                    p.SetValue(Filter, string.Empty);
                }
            }
        });

        /// <summary>
        /// The command to transfer the object to the "In archive" status 
        /// </summary>
        public ICommand OrderStatus => new RelayCommand<object>(obj =>
        {
            // Request to change the status of an object 
            if (Message.Show("Архивирование", "Вы уверены в изменении статуса "
                + selectedOrders.Count + " шт. объектов ",
                MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                // Add the selected items to the collection
                foreach (NewWrite t in selectedOrders)
                {
                    // Change the status of the order availability 
                    t.NoActive = "Архив";

                    ConnectionClass.hubConnection.InvokeAsync("StartChangeWork", t);
                }
            }
        });

        /// <summary>
        /// Command for updating data in the DataGrid table 
        /// </summary>
        public ICommand Refresh => new RelayCommand<object>(obj =>
        {
            MainObject.AdminWorks = null;

            UsersWorks = null;

            ConnectionClass.hubConnection.InvokeAsync("UpdateAll", MainObject.Access);

            Status = $"Обновлено в {DateTime.Now.ToShortTimeString()}";
        });

        public AllWorksViewModel(MainObject mainObject)
        {
            SignalRActions();

            MainObject = mainObject;

            UsersWorks = new ListCollectionView(mainObject.AdminWorks);

            Filter = new AllWorksFilter(UsersWorks);

            dispatcher = Application.Current.Dispatcher;
        }

        protected override void SignalRActions()
        {
            ConnectionClass.hubConnection.On<NewWrite>("UpdateWorks", (newWork) =>
            {
                Application.Current.Dispatcher.Invoke(() => MainObject.AdminWorks.Insert(0, newWork));
            });

            ConnectionClass.hubConnection.On<MainObject>("UpdateRequest", (main) =>
            {
                dispatcher.Invoke(() => MainObject = main);
                dispatcher.Invoke(() => UsersWorks = new ListCollectionView(MainObject.AdminWorks));
                dispatcher.Invoke(() => Filter = new AllWorksFilter(UsersWorks));
            });

            ConnectionClass.hubConnection.On<NewWrite>("ChangedWork", (changedWork) =>
            {
                dispatcher.Invoke(() =>
                {
                    foreach (var a in MainObject.AdminWorks.Where(x => x.Id == changedWork.Id).FirstOrDefault().GetType().GetProperties())
                    {
                        foreach (var c in changedWork.GetType().GetProperties())
                        {
                            if (a.Name == c.Name)
                            {
                                a.SetValue(MainObject.AdminWorks.Where(x => x.Id == changedWork.Id).FirstOrDefault(), c.GetValue(changedWork));

                                continue;
                            }
                        }
                    }
                });
            });

            ConnectionClass.hubConnection.On<NewWrite>("ChangedError", (changedWork) => Message.Show("Ошибка записи", "Ошибка записи", MessageBoxButton.OK));
        }

        /// <summary>
        /// Method of exporting Excel File 
        /// </summary>
        private async void GetExele()
        {
            Status = "В работе";

            // Create a temporary collection 
            List<NewWrite> tempCol = GetWorksCount();
           
            SaveOpenFile saveOpen = new SaveOpenFile();

            string fileName = GetFileName(Filter.DateOne, Filter.DateTwo);

            string path = saveOpen.SaveDialog(fileName);

            if (!string.IsNullOrEmpty(path))
            {
                bool temp = await Task.Run(() => ExcelUsage.GetExcel(tempCol, path));

                if (temp == true)
                {
                    Status = $"Сохранено";
                }
                else { Status = "Ошибка сохранения";}
            }
            else { Status = "Сохранение отменено"; }
        }

        /// <summary>
        /// The method gets a list of selected elements and forms a collection from them 
        /// </summary>
        /// <param name="list"></param>
        public static void SelectionChanged(IList list)
        {
            selectedOrders = new ObservableCollection<NewWrite>();

            foreach (var item in list)
            {
                selectedOrders.Add(item as NewWrite);
            }
        }

        /// <summary>
        /// The method returns the file name based on the selected dates in the filter by date 
        /// </summary>
        /// <param name="dateOne"></param>
        /// <param name="dateTwo"></param>
        /// <returns></returns>
        public static string GetFileName(DateTime? dateOne, DateTime? dateTwo)
        {
            if (dateOne != null && dateTwo != null)
            {
                // Filename if dates were selected 
                return $"Отчет {dateOne.Value.ToString("dd.MM.yyyy")} - {dateTwo.Value.ToString("dd.MM.yyyy")}";
            }
            else if(dateOne != null)
            {
                return $"отчет c {dateOne.Value.ToString("dd.MM.yyyy")} - {DateTime.Now.ToString("dd.MM.yyyy")}";
            }
            else if (dateTwo != null)
            {
                return $"отчет по {dateTwo.Value.ToString("dd.MM.yyyy")}";
            }
            else
            {
                // Default filename if no dates are selected 
                return "Отчет за все время";
            }
        }

        /// <summary>
        /// The method checks the number of selected objects 
        /// </summary>
        /// <returns></returns>
        private List<NewWrite> GetWorksCount()
        {
            // Condition under which all works with or without filters are uploaded to the Excel file
            if (selectedOrders.Count <= 2)
            {
                return MainObject.AdminWorks.ToList();
            }
            // Condition under which only selected jobs are unloaded 
            else
            {
                return selectedOrders.ToList();
            }
        }
    }
}
