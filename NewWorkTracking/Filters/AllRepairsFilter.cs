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
    /// Contains the logic for filtering by repair cards
    /// </summary>
    class AllRepairsFilter : PropertyChangeClass, IFilters
    {
        /// <summary>
        /// The collection of the view to filter 
        /// </summary>
        private ListCollectionView allRepairs;
       
        private DateTime? dateOne;
        /// <summary>
        /// First date of the filtering period 
        /// </summary>
        public DateTime? DateOne 
        {
            get => dateOne;
            set { dateOne = value; OnPropertyChanged(nameof(DateOne)); allRepairs.Filter = new Predicate<object>(ItemsFilter); }
        }
      
        private DateTime? dateTwo;
        /// <summary>
        /// Second date of the filtering period 
        /// </summary>
        public DateTime? DateTwo 
        {
            get => dateTwo;
            set { dateTwo = value; OnPropertyChanged(nameof(DateTwo)); allRepairs.Filter = new Predicate<object>(ItemsFilter); }
        }

        private string osTypeFilter;
        /// <summary>
        /// Filter by asset type 
        /// </summary>
        public string OsTypeFilter
        {
            get => osTypeFilter;
            set { osTypeFilter = value; OnPropertyChanged(nameof(OsTypeFilter)); allRepairs.Filter = new Predicate<object>(ItemsFilter); }
        }

        private string selectedStatus;
        /// <summary>
        /// Filter by selected repair status 
        /// </summary>
        public string SelectedStatus
        {
            get => selectedStatus;
            set { selectedStatus = value; OnPropertyChanged(nameof(SelectedStatus)); allRepairs.Filter = new Predicate<object>(ItemsFilter); }
        }

        private string selectedModel;
        /// <summary>
        /// Filter by the selected device model 
        /// </summary>
        public string SelectedModel
        {
            get => selectedModel;
            set { selectedModel = value; OnPropertyChanged(nameof(selectedModel)); allRepairs.Filter = new Predicate<object>(ItemsFilter); }
        }

        private string selectedScOks;
        /// <summary>
        /// Filter by SC OKS 
        /// </summary>
        public string SelectedScOks
        {
            get => selectedScOks;
            set { selectedScOks = value; OnPropertyChanged(nameof(SelectedScOks)); allRepairs.Filter = new Predicate<object>(ItemsFilter); }
        }

        private string selectedKaProvider;
        /// <summary>
        /// Filter by vendor's CA 
        /// </summary>
        public string SelectedKaProvider
        {
            get => selectedKaProvider;
            set { selectedKaProvider = value; OnPropertyChanged(nameof(SelectedKaProvider)); allRepairs.Filter = new Predicate<object>(ItemsFilter); }
        }

        private string selectedOsName;
        /// <summary>
        /// Filter by asset type 
        /// </summary>
        public string SelectedOsName
        {
            get => selectedOsName;
            set { selectedOsName = value; OnPropertyChanged(nameof(SelectedOsName)); allRepairs.Filter = new Predicate<object>(ItemsFilter); }
        }

        private string selectedWaranty;
        /// <summary>
        /// Filter by warranty type 
        /// </summary>
        public string SelectedWaranty
        {
            get => selectedWaranty;
            set { selectedWaranty = value; OnPropertyChanged(nameof(SelectedWaranty)); allRepairs.Filter = new Predicate<object>(ItemsFilter); }
        }

        private string searchLine;
        /// <summary>
        /// Search line
        /// </summary>
        public string SearchLine
        {
            get => searchLine;
            set { searchLine = value; OnPropertyChanged(nameof(SearchLine)); allRepairs.Filter = new Predicate<object>(Search); }
        }

        /// <summary>
        /// The constructor takes a collection of the view to filter
        /// </summary>
        /// <param name="worksView"></param>
        public AllRepairsFilter(ListCollectionView worksView)
        {
            allRepairs = worksView;
        }

        /// <summary>
        /// The method filters data by the selected parameters 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private bool ItemsFilter(object obj)
        {
            if (obj is RepairClass temp)
            {
                return ((temp.Date >= DateOne || DateOne == null)
                    && (temp.Date <= DateTwo || DateTwo == null))
                    && (temp.OsName == SelectedOsName || string.IsNullOrWhiteSpace(SelectedOsName))
                    && (temp.Status == SelectedStatus || string.IsNullOrWhiteSpace(SelectedStatus))
                    && (temp.Model == SelectedModel || string.IsNullOrWhiteSpace(SelectedModel))
                    && (temp.KaProvider == SelectedKaProvider || string.IsNullOrWhiteSpace(SelectedKaProvider))
                    && (temp.Warranty == SelectedWaranty || string.IsNullOrWhiteSpace(SelectedWaranty))
                    && (temp.ScOks == SelectedScOks || string.IsNullOrWhiteSpace(SelectedScOks));
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// The method searches for keywords in all displayed fields of the object 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private bool Search(object obj)
        {
            if (obj is RepairClass temp)
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
