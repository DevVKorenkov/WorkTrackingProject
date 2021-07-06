using NewWorkTracking.Models;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using WorkTrackingLib.Models;
using System.Windows.Threading;
using System.Windows.Input;
using WorkTrackingLib.ProcessClasses;
using System.Collections.ObjectModel;
using NewWorkTracking.Windows;
using NewWorkTracking.Actions;
using NewWorkTracking.Connection;
using NewWorkTracking.Filters;

namespace NewWorkTracking.ViewModels
{
    /// <summary>
    /// Data context of the device tab 
    /// </summary>
    class DevicesViewModel : AbstractViewModel
    {
        /// <summary>
        /// Collection of imported data from Excel file 
        /// </summary>
        private List<RepairClass> LoadedRepairs;

        /// <summary>
        /// A copy of the selected device
        /// </summary>
        private Devices selectedDeviceCopy;

        private Devices selectedDevice;
        /// <summary>
        /// Selected device
        /// </summary>
        public Devices SelectedDevice
        {
            get => selectedDevice;
            set { selectedDevice = value; OnPropertyChanged(nameof(SelectedDevice)); selectedDeviceCopy = (Devices)SelectedDevice.Clone(); }
        }

        private RepairClass selectedRepair;
        /// <summary>
        /// Selected repair 
        /// </summary>
        public RepairClass SelectedRepair
        {
            get => selectedRepair;
            set { selectedRepair = value; OnPropertyChanged(nameof(SelectedRepair)); }
        }

        private string deviceName;
        /// <summary>
        /// Property of the device model 
        /// </summary>
        public string DeviceName
        {
            get => deviceName;
            set { deviceName = value; OnPropertyChanged(nameof(DeviceName)); }
        }

        private string invNumber;
        /// <summary>
        /// Property of the device inventory number
        /// </summary>
        public string InvNumber
        {
            get => invNumber;
            set { invNumber = value; OnPropertyChanged(nameof(InvNumber)); }
        }

        private string osName;
        /// <summary>
        /// Property of the device type 
        /// </summary>
        public string OsName
        {
            get => osName;
            set { osName = value; OnPropertyChanged(nameof(OsName)); }
        }

        /// <summary>
        /// Dispatcher variable for accessing objects from another thread
        /// </summary>
        private Dispatcher dispather;

        /// <summary>
        /// Command for recording repair changes 
        /// </summary>
        public ICommand ChangeRepair => new RelayCommand<object>(obj => ConnectionClass.hubConnection.InvokeAsync("StartChangeRepair", SelectedRepair));

        /// <summary>
        /// Command to write device changes 
        /// </summary>
        public ICommand ChangeDevice => new RelayCommand<object>(obj => ConnectionClass.hubConnection.InvokeAsync("StartChangeDevice", SelectedDevice));

        /// <summary>
        /// Command for adding a new device 
        /// </summary>
        public ICommand AddNewDevice => new RelayCommand<object>(obj => ConnectionClass.hubConnection.InvokeAsync("RunAddDevice", new Devices() { DeviceName = DeviceName, InvNumber = InvNumber, OsName = OsName }));

        /// <summary>
        /// Command to import data from excel
        /// </summary>
        public ICommand LoadRepairs => new RelayCommand<object>(obj =>
        {
            SaveOpenFile saveOpenFile = new SaveOpenFile();

            ExcelUsage excelUsage = new ExcelUsage();

            var path = saveOpenFile.OpenDialog();

            // Condition of command execution
            if (!string.IsNullOrEmpty(path))
            {
                // start reading the file on another thread 
                Task.Run(() =>
                {
                    // Execute method to import data from excel file 
                    string output = excelUsage.LoadExcel(path, LoadedRepairs);

                    if (output.Contains("считан"))
                    {
                        // Condition for executing a request to the server to write data to the database 
                        if (dispather.Invoke(() => Message.Show("Внимание", $"{output} Объектов в файле: {LoadedRepairs.Count()}. Загрузить в БД?", MessageBoxButton.YesNo) == MessageBoxResult.Yes))
                        {
                            foreach (var t in LoadedRepairs)
                            {
                                ConnectionClass.hubConnection.InvokeAsync("RunAddRepair", t);
                            }

                            LoadedRepairs.Clear();
                        }
                        else
                        {
                            LoadedRepairs.Clear();
                        }
                    }
                    // Action on error execution of the excelUsage.LoadExcel method
                    else
                    {
                        dispather.Invoke(() => Message.Show("Внимание", output, MessageBoxButton.OK));
                    }
                });
            }
        });

        /// <summary>
        /// Command to start the window for adding a new repair 
        /// </summary>
        public ICommand OpenCleanRepairCard => new RelayCommand<object>(obj =>
        {
            // Condition under which the window for adding a new job will open 
            if (SelectedDevice != null)
            {
                RepairCardViewModel cardViewModel = new RepairCardViewModel(MainObject, SelectedDevice);

                RepairCard repairCard = new RepairCard()
                {
                    DataContext = cardViewModel
                };

                repairCard.ShowDialog();
            }
            else
            {
                Message.Show("", "Выберите устройство", MessageBoxButton.OK);
            }
        });

        public ICommand OpenSwitchDevicerepair => new RelayCommand<object>(obj =>
        {
            SwitchDevicesRepairViewModel switchDevices = new SwitchDevicesRepairViewModel(SelectedRepair, MainObject.Devices);

            SwitchDeviceRepairWindow switchDeviceWindow = new SwitchDeviceRepairWindow()
            {
                DataContext = switchDevices
            };

            switchDeviceWindow.ShowDialog();
        });

        public DevicesViewModel(MainObject mainObject)
        {
            SignalRActions();
            LoadedRepairs = new List<RepairClass>();
            ActiveVisibility = Visibility.Collapsed;
            MainObject = mainObject;
            UsersWorks = new ListCollectionView(MainObject.Devices);
            Filter = new DeviceFilter(UsersWorks);
            dispather = Application.Current.Dispatcher;
        }

        /// <summary>
        /// Method for subscribing to server messages 
        /// </summary>
        protected override void SignalRActions()
        {
            ConnectionClass.hubConnection.On<Devices>("UpdateDevices", (newDevice) =>
            {
                dispather.Invoke(() => MainObject.Devices.Insert(0, newDevice));
            });

            ConnectionClass.hubConnection.On<bool>("DeviceNotAdded", (result) => dispather.Invoke(() =>
            Message.Show("Ошибка добавления", "Ошибка добавления. Возможно такое устройство уже существует", MessageBoxButton.OK)));

            ConnectionClass.hubConnection.On<bool>("DeviceChangedError", (result) => dispather.Invoke(() =>
            {
                Message.Show("Ошибка изменения", $@"Вы не можете изменить инв.номер выбранного устройства на ""{SelectedDevice.InvNumber}"", т.к. устройство с таким инв.номером уже существует", MessageBoxButton.OK);

                SelectedDevice = selectedDeviceCopy;
            }));

            ConnectionClass.hubConnection.On<Devices>("ChangedDevice", (changeDevice) =>
            {
                dispather.Invoke(() =>
                {
                    foreach (var a in MainObject.Devices.Where(x => x.Id == changeDevice.Id).FirstOrDefault().GetType().GetProperties())
                    {
                        foreach (var c in changeDevice.GetType().GetProperties())
                        {
                            if (a.Name == c.Name)
                            {
                                a.SetValue(MainObject.Devices.Where(x => x.Id == changeDevice.Id).FirstOrDefault(), c.GetValue(changeDevice));

                                continue;
                            }
                        }
                    }
                });
            });

            ConnectionClass.hubConnection.On<RepairClass>("UpdateRepairs", (newRepair) =>
            {
                foreach (var r in MainObject.Devices)
                {
                    if (r.Id == newRepair.DeviceId)
                    {
                        dispather.Invoke(() => { r.Repairs.Add(newRepair); r.Repairs = new ObservableCollection<RepairClass>(r.Repairs); });

                        break;
                    }
                }
            });

            ConnectionClass.hubConnection.On<RepairClass>("ChangedRepair", (changeRepair) =>
            {
                dispather.Invoke(() =>
                {
                    foreach (var d in MainObject.Devices)
                    {
                        d.Repairs.Remove(d.Repairs.Where(x => x.Id == changeRepair.Id).FirstOrDefault());
                    }

                    foreach (var d in MainObject.Devices)
                    {
                        if (changeRepair.DeviceId == d.Id)
                        {
                            d.Repairs.Add(changeRepair);

                            d.Repairs = new ObservableCollection<RepairClass>(d.Repairs);

                            break;
                        }
                    }
                });
            });
        }
    }
}
