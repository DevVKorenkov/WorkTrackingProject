using NewWorkTracking.Models;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using WorkTrackingLib.Models;
using System.Windows.Threading;
using System.Windows.Input;
using WorkTrackingLib.ProcessClasses;
using System.Collections.ObjectModel;
using NewWorkTracking.Connection;
using NewWorkTracking.Filters;
using NewWorkTracking.Actions;

namespace NewWorkTracking.ViewModels
{
    /// <summary>
    /// View model of displaying all repairs
    /// </summary>
    class RepairsViewModel : AbstractViewModel
    {
        private RepairCardDataContext repairContext;
        /// <summary>
        /// Repair data context
        /// </summary>
        public RepairCardDataContext RepairContext
        {
            get => repairContext;
            set { repairContext = value; OnPropertyChanged(nameof(RepairContext)); }
        }

        private List<string> filterModelList;
        /// <summary>
        /// Device filter
        /// </summary>
        public List<string> FilterModelList
        {
            get => filterModelList;
            set { filterModelList = value; OnPropertyChanged(nameof(FilterModelList)); }
        }

        private List<string> filterProviderList;
        /// <summary>
        /// Supplier filter
        /// </summary>
        public List<string> FilterProviderList
        {
            get => filterProviderList;
            set { filterProviderList = value; OnPropertyChanged(nameof(FilterProviderList)); }
        }

        private List<string> filterRepairTypeList;
        /// <summary>
        /// Repair type filter
        /// </summary>
        public List<string> FilterRepairTypeList
        {
            get => filterRepairTypeList;
            set { filterRepairTypeList = value; OnPropertyChanged(nameof(FilterRepairTypeList)); }
        }

        private RepairClass selectedRepair;
        /// <summary>
        /// Selected repair object
        /// </summary>
        public RepairClass SelectedRepair
        {
            get => selectedRepair;
            set { selectedRepair = value; OnPropertyChanged(nameof(SelectedRepair)); }
        }

        private ObservableCollection<RepairClass> selectedRepairs;
        /// <summary>
        /// Selected repair objects
        /// </summary>
        public ObservableCollection<RepairClass> SelectedRepairs
        {
            get => selectedRepairs;
            set { selectedRepairs = value; OnPropertyChanged(nameof(SelectedRepairs)); }
        }

        private Dispatcher dispatcher;

        /// <summary>
        /// Column hide object
        /// </summary>
        public HideColumns HideColumns { get; set; }

        /// <summary>
        /// Repair card opening command
        /// </summary>
        public ICommand OpenRepairCard => new RelayCommand<object>(obj =>
        {
            Actively = typeof(RepairCardDataContext).ToString();

            RepairContext.SelectedItem = SelectedRepair;

            RepairContext.Combobox = MainObject.ComboBox;

            RepairContext.RepairCardVis = Visibility.Visible;

            ActiveVisibility = Visibility.Collapsed;
        });

        /// <summary>
        /// Record command
        /// </summary>
        public ICommand ChangeRepair => new RelayCommand<object>(obj =>
        {
            ConnectionClass.hubConnection.InvokeAsync("StartChangeRepair", SelectedRepair);
        });

        /// <summary>
        /// Filter cleaning command
        /// </summary>
        public ICommand RefreshFilters => new RelayCommand<object>(obj =>
        {
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
        /// Opening a blank repair card command
        /// </summary>
        public ICommand OpenCleanRepairCard => new RelayCommand<object>(obj =>
        {
            RepairCardViewModel cardViewModel = new RepairCardViewModel(MainObject);

            RepairCard repairCard = new RepairCard()
            {
                DataContext = cardViewModel
            };

            repairCard.ShowDialog();
        });

        /// <summary>
        /// Formation of a table for budgeting
        /// </summary>
        public ICommand BudgetingRepairs => new RelayCommand<object>(obj =>
        {
            foreach (PropertyInfo h in HideColumns.GetType().GetProperties())
            {
                if (h.Name == "ProviderOrder" || h.Name == "RepairBill" || h.Name == "KaRepair" || h.Name == "ScOks")
                {
                    h.SetValue(HideColumns, true);
                }
                else
                {
                    h.SetValue(HideColumns, false);
                }
            }
        });

        public ICommand SeeAll => new RelayCommand<object>(obj =>
        {
            CreateHidenColumns();
        });

        /// <summary>
        /// Data dump command
        /// </summary>
        public ICommand GetExcel => new RelayCommand<object>(obj =>
        {
            var tempItems = (System.Collections.IList)obj;

            SelectedRepairs = new ObservableCollection<RepairClass>(tempItems?.Cast<RepairClass>());

            SaveOpenFile saveOpen = new SaveOpenFile();

            string fileName = AllWorksViewModel.GetFileName(Filter.DateOne, Filter.DateTwo);

            string path = saveOpen.SaveDialog(fileName);

            if (SelectedRepairs.Count <= 2)
            {
                if (Filter.DateOne != null || Filter.DateTwo != null)
                {
                    ExcelUsage.GetExcel(UsersWorks.Cast<RepairClass>().ToList(), path);

                    return;
                }

                ExcelUsage.GetExcel(MainObject.Repairs.ToList(), path);
            }
            else
            {
                ExcelUsage.GetExcel(SelectedRepairs.ToList(), path);
            }
        });

        public RepairsViewModel(MainObject mainObject)
        {
            SignalRActions();
            RepairContext = new RepairCardDataContext();
            ActiveVisibility = Visibility.Collapsed;
            MainObject = mainObject;
            UsersWorks = new ListCollectionView(MainObject.Repairs);
            Filter = new AllRepairsFilter(UsersWorks);
            HideColumns = new HideColumns();
            dispatcher = Application.Current.Dispatcher;
            GetFiltersLists();
            CreateHidenColumns();
            GetDataGridElements();
        }

        protected override void SignalRActions()
        {
            ConnectionClass.hubConnection.On<RepairClass>("UpdateRepairs", (newRepair) =>
            {
                dispatcher.Invoke(() => MainObject.Repairs.Insert(0, newRepair));
                GetFiltersLists();
            });

            ConnectionClass.hubConnection.On<RepairClass>("ChangedRepair", (changeRepair) =>
            {
                dispatcher.Invoke(() =>
                {
                    foreach (var a in MainObject.Repairs.Where(x => x.Id == changeRepair.Id).FirstOrDefault().GetType().GetProperties())
                    {
                        foreach (var c in changeRepair.GetType().GetProperties())
                        {
                            if (a.Name == c.Name)
                            {
                                a.SetValue(MainObject.Repairs.Where(x => x.Id == changeRepair.Id).FirstOrDefault(), c.GetValue(changeRepair));

                                continue;
                            }
                        }
                    }
                });

                GetFiltersLists();
            });
        }

        /// <summary>
        /// Hides columns in a table
        /// </summary>
        private void CreateHidenColumns()
        {
            foreach (PropertyInfo h in HideColumns.GetType().GetProperties())
            {
                h.SetValue(HideColumns, true);
            }
        }

        /// <summary>
        /// Gets the elements of a table
        /// </summary>
        private void GetDataGridElements()
        {
            FrameworkElement.DataContextProperty.AddOwner(typeof(DataGridColumn));

            FrameworkElement.DataContextProperty.OverrideMetadata(typeof(DataGrid),
                    new FrameworkPropertyMetadata
                       (null, FrameworkPropertyMetadataOptions.Inherits,
                       new PropertyChangedCallback(OnDataContextChanged)));
        }

        public void OnDataContextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DataGrid grid = d as DataGrid;
            if (grid != null)
            {
                foreach (DataGridColumn col in grid.Columns)
                {
                    col.SetValue(FrameworkElement.DataContextProperty, e.NewValue);
                }
            }
        }

        /// <summary>
        /// Gets lists for filters
        /// </summary>
        private void GetFiltersLists()
        {
            FilterModelList = MainObject.Repairs.Select(x => x.Model).Distinct().ToList();
            FilterProviderList = MainObject.Repairs.Select(x => x.KaProvider).Distinct().ToList();
            FilterRepairTypeList = MainObject.Repairs.Select(x => x.Warranty).Distinct().ToList();
        }
    }
}
