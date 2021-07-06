using NewWorkTracking.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using WorkTrackingLib;
using WorkTrackingLib.Models;

namespace NewWorkTracking.Filters
{
    /// <summary>
    /// Contains the logic for filtering by devices cards
    /// </summary>
    class DeviceFilter : PropertyChangeClass, IFilters
    {
        public DateTime? DateOne { get; set; } // Not used in device filtering 
        public DateTime? DateTwo { get; set; } //

        private ListCollectionView devices;

        private string searchLine;
        /// <summary>
        /// Search line 
        /// </summary>
        public string SearchLine
        {
            get => searchLine;
            set { searchLine = value; OnPropertyChanged(nameof(SearchLine)); devices.Filter = new Predicate<object>(Search); }
        }

        public string OsTypeFilter { get ; set; } // Not used in device filtering 

        public DeviceFilter(ListCollectionView worksView)
        {
            devices = worksView;
        }

        /// <summary>
        /// Search method 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private bool Search(object obj)
        {
            if (obj is Devices temp)
            {
                bool result = false;

                foreach (var p in temp.GetType().GetProperties())
                {
                    if (p.GetValue(temp) != null && p.GetValue(temp).ToString().ToLower().Contains(SearchLine.ToLower()))
                    {
                        result = true;
                    }
                }

                return result;
            }
            else
            {
                return false;
            }
        }
    }
}
