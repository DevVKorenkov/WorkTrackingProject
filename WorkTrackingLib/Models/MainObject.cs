using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkTrackingLib.Models
{
    /// <summary>
    /// The class forms an object with all the necessary data
    /// </summary>
    public class MainObject : PropertyChangeClass
    {
        #region Свойства

        private AccessModel access;
        /// <summary>
        /// User access data property
        /// </summary>
        public AccessModel Access
        {
            get { return access; }
            set { access = value; OnPropertyChanged(nameof(Access)); }
        }

        private ObservableCollection<NewWrite> adminWorks;
        /// <summary>
        /// Property collection of data on added jobs
        /// </summary>
        public ObservableCollection<NewWrite> AdminWorks 
        {
            get { return adminWorks; }
            set { adminWorks = value; OnPropertyChanged(nameof(AdminWorks)); }
        }

        private ObservableCollection<Devices> devices;
        public ObservableCollection<Devices> Devices
        {
            get => devices;
            set { devices = value; OnPropertyChanged(nameof(Devices)); }
        }
        
        private ObservableCollection<RepairClass> repairs;
        public ObservableCollection<RepairClass> Repairs
        {
            get => repairs;
            set { repairs = value; OnPropertyChanged(nameof(Repairs)); }
        }

        private ComboboxDataSource comboBox;
        /// <summary>
        /// Lists property
        /// </summary>
        public ComboboxDataSource ComboBox 
        {
            get { return comboBox; }
            set { comboBox = value; OnPropertyChanged(nameof(ComboBox)); }
        }

        #endregion

        #region Constructors

        public MainObject(AccessModel access, 
            ObservableCollection<NewWrite> adminWorks, 
            ObservableCollection<Devices> devices, 
            ObservableCollection<RepairClass> repairs, 
            ComboboxDataSource comboBox)
        {
            this.adminWorks = new ObservableCollection<NewWrite>();
            this.devices = new ObservableCollection<Devices>();
            this.repairs = new ObservableCollection<RepairClass>();

            this.Access = access;
            this.AdminWorks = adminWorks;
            this.Devices = devices;
            this.Repairs = repairs;
            this.ComboBox = comboBox;
        }

        public MainObject()
        {
            this.adminWorks = new ObservableCollection<NewWrite>();
            this.devices = new ObservableCollection<Devices>();
            this.repairs = new ObservableCollection<RepairClass>();
        }

        #endregion
    }
}
