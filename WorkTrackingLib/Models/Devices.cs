using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkTrackingLib.Models
{
    public class Devices : PropertyChangeClass, ICloneable
    {
        public int Id { get; set; }

        private ObservableCollection<RepairClass> repairs;
        [NotMapped]
        public ObservableCollection<RepairClass> Repairs 
        {
            get => repairs;
            set { repairs = value; OnPropertyChanged(nameof(Repairs)); }
        }
        
        //private ObservableCollection<string> scOks;
        //[NotMapped]
        //public ObservableCollection<string> ScOks 
        //{
        //    get => scOks;
        //    set { scOks = value; OnPropertyChanged(nameof(ScOks)); }
        //}

        private string deviceName = string.Empty;
        public string DeviceName 
        {
            get { return deviceName; }
            set { deviceName = value; OnPropertyChanged(nameof(DeviceName)); }
        }

        private string invNumber;
        public string InvNumber
        {
            get { return invNumber; }
            set { invNumber = value; OnPropertyChanged(nameof(InvNumber)); }
        }

        private string osName = string.Empty;
        public string OsName
        {
            get => osName;
            set { osName = value; OnPropertyChanged(nameof(OsName)); }
        }

        public Devices()
        {
            Repairs = new ObservableCollection<RepairClass>();
        }

        /// <summary>
        /// The method implements the object cloning interface
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
