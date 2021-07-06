using Microsoft.AspNetCore.SignalR.Client;
using NewWorkTracking.Actions;
using NewWorkTracking.Connection;
using NewWorkTracking.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WorkTrackingLib;
using WorkTrackingLib.Models;
using WorkTrackingLib.ProcessClasses;

namespace NewWorkTracking.ViewModels
{
    /// <summary>
    /// Contains the logic for moving a repair object from one device to another 

    /// </summary>
    class SwitchDevicesRepairViewModel : PropertyChangeClass
    {
        /// <summary>
        /// Selected renovation object 

        /// </summary>
        public RepairClass SelectedRepair { get; set; }

        /// <summary>
        /// Device List
        /// </summary>
        public ObservableCollection<Devices> Devices { get; set; }

        private string description;
        /// <summary>
        /// 
        /// </summary>
        public string Description
        {
            get => description;
            set { description = value; OnPropertyChanged(nameof(Description)); }
        }

        private string selectedDeviceInv;
        /// <summary>
        /// Inventory number of the selected device 
        /// </summary>
        public string SelectedDeviceInv
        {
            get => selectedDeviceInv;
            set
            {
                selectedDeviceInv = value;
                OnPropertyChanged(nameof(SelectedDeviceInv));
                tempDevice = Devices.Where(x => x.InvNumber == SelectedDeviceInv).FirstOrDefault();
                if (tempDevice != null)
                    Description = $"Вы выбрали {tempDevice.DeviceName}";
            }
        }

        private Devices tempDevice;

        /// <summary>
        /// The command to transfer a repair object to another device 
        /// </summary>
        public ICommand SwitchDeviceRepair => new RelayCommand<object>(obj =>
        {
            if (Message.Show("Внимание", $"Переместить ремонт {SelectedRepair.DiagNumber}, инв.номер {SelectedRepair.InvNumber} к усторйству {tempDevice.DeviceName}, Инв.номер {tempDevice.InvNumber}?",
                System.Windows.MessageBoxButton.YesNo) == System.Windows.MessageBoxResult.Yes)
            {
                SelectedRepair.DeviceId = tempDevice.Id;
                SelectedRepair.InvNumber = tempDevice.InvNumber;

                ConnectionClass.hubConnection.InvokeAsync("StartChangeRepair", SelectedRepair);

                WindowUsage.CloseWindow("SwitchDeviceRepairWindowXaml");
            }
        });

        public ICommand CloseWindow => new RelayCommand<object>(obj =>
        {
            WindowUsage.CloseWindow("SwitchDeviceRepairWindowXaml");
        });

        public SwitchDevicesRepairViewModel(RepairClass selectedRepair, ObservableCollection<Devices> devices)
        {
            SelectedRepair = selectedRepair;
            Devices = devices;
        }

        public SwitchDevicesRepairViewModel()
        { 
        }
    }
}
