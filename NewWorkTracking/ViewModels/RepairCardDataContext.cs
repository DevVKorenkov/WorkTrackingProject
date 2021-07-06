using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WorkTrackingLib;
using WorkTrackingLib.Models;

namespace NewWorkTracking.ViewModels
{
    class RepairCardDataContext : PropertyChangeClass
    {
        private Visibility repairCardVis;
        public Visibility RepairCardVis
        {
            get => repairCardVis;
            set { repairCardVis = value; OnPropertyChanged(nameof(RepairCardVis)); }
        }

        private ComboboxDataSource combobox;
        public ComboboxDataSource Combobox
        {
            get => combobox;
            set { combobox = value; OnPropertyChanged(nameof(Combobox)); }
        }

        private RepairClass selectedItem;
        public RepairClass SelectedItem
        {
            get => selectedItem;
            set { selectedItem = value; OnPropertyChanged(nameof(SelectedItem)); }
        }

        public RepairCardDataContext()
        {
            RepairCardVis = Visibility.Collapsed;
        }
    }
}
