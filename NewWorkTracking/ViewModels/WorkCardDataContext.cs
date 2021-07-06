using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WorkTrackingLib;
using WorkTrackingLib.Models;

namespace NewWorkTracking.ViewModels
{
    class WorkCardDataContext : PropertyChangeClass
    {
        private Visibility workCardVis;
        public Visibility WorkCardVis
        {
            get => workCardVis;
            set { workCardVis = value; OnPropertyChanged(nameof(WorkCardVis)); }
        }

        private ComboboxDataSource combobox;
        public ComboboxDataSource Combobox 
        {
            get => combobox;
            set { combobox = value; OnPropertyChanged(nameof(Combobox)); }
        }

        private NewWrite selectedItem;
        public NewWrite SelectedItem
        {
            get => selectedItem;
            set { selectedItem = value; OnPropertyChanged(nameof(SelectedItem)); }
        }

        public WorkCardDataContext()
        {
            WorkCardVis = Visibility.Collapsed;
        }
    }
}
