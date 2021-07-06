using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using WorkTrackingLib;
using WorkTrackingLib.Models;
using NewWorkTracking.Interfaces;
using System.Windows;

namespace NewWorkTracking.ViewModels
{
    abstract class AbstractViewModel : PropertyChangeClass
    {
        public string Actively { get; set; }

        private IFilters filter;
        /// <summary>
        /// Filter property 
        /// </summary>
        public IFilters Filter 
        {
            get => filter;
            set { filter = value; OnPropertyChanged(nameof(Filter)); }
        }

        private ListCollectionView usersWorks;
        /// <summary>
        /// List of view of data output to table
        /// </summary>
        public ListCollectionView UsersWorks
        {
            get => usersWorks;
            set { usersWorks = value; OnPropertyChanged(nameof(UsersWorks)); }
        }

        private MainObject mainObject;
        /// <summary>
        /// Property of the main object of the received data 
        /// </summary>
        public MainObject MainObject 
        {
            get => mainObject;
            set { mainObject = value; OnPropertyChanged(nameof(MainObject)); }
        }

        private Visibility activeVisibility;
        /// <summary>
        /// Tab visibility property 
        /// </summary>
        public Visibility ActiveVisibility
        {
            get => activeVisibility;
            set { activeVisibility = value; OnPropertyChanged(nameof(activeVisibility)); }
        }

        public AbstractViewModel()
        {
            ActiveVisibility = Visibility.Collapsed;

            MainObject = new MainObject();
        }

        /// <summary>
        /// Method for subscribing to server responses 
        /// </summary>
        protected abstract void SignalRActions();
    }
}
