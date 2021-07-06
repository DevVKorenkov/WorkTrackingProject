using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkTrackingLib.Models
{
    /// <summary>
    /// The class provides lists for the admin window
    /// </summary>
    public class ControlSqlObjects : PropertyChangeClass
    {
        #region Properties

        private ObservableCollection<AccessModel> admins;
        /// <summary>
        /// User list property
        /// </summary>
        public ObservableCollection<AccessModel> Admins 
        {
            get { return admins; }
            set { admins = value; OnPropertyChanged(nameof(Admins)); }
        }

        private ObservableCollection<Osp> ospCol;
        /// <summary>
        /// Property of the list of separate subdivisions
        /// </summary>
        public ObservableCollection<Osp> OspCol
        {
            get { return ospCol; }
            set { ospCol = value; OnPropertyChanged(nameof(OspCol)); }
        }

        private ObservableCollection<OsType> osTypeCol;
        /// <summary>
        /// Property of the OS type list
        /// </summary>
        public ObservableCollection<OsType> OsTypeCol
        {
            get { return osTypeCol; }
            set { osTypeCol = value; OnPropertyChanged(nameof(OsTypeCol)); }
        }

        private ObservableCollection<Results> resultsCol;
        /// <summary>
        /// Property of the list of results
        /// </summary>
        public ObservableCollection<Results> ResultsCol
        {
            get { return resultsCol; }
            set { resultsCol = value; OnPropertyChanged(nameof(ResultsCol)); }
        }

        private ObservableCollection<Why> whyCol;
        /// <summary>
        /// Property of the list of reasons for calling
        /// </summary>
        public ObservableCollection<Why> WhyCol
        {
            get { return whyCol; }
            set { whyCol = value; OnPropertyChanged(nameof(WhyCol)); }
        }
        
        private ObservableCollection<ScOks> scOksCol;
        /// <summary>
        /// Property of the list of reasons for calling
        /// </summary>
        public ObservableCollection<ScOks> ScOksCol
        {
            get { return scOksCol; }
            set { scOksCol = value; OnPropertyChanged(nameof(ScOksCol)); }
        }

        #endregion

        #region Constructors

        public ControlSqlObjects(ObservableCollection<AccessModel> accessModel,
            ObservableCollection<Osp> ops, ObservableCollection<OsType> osType,
            ObservableCollection<Results> results, ObservableCollection<Why> why, ObservableCollection<ScOks> scOks)
        {
            Admins = accessModel;
            OspCol = ops;
            OsTypeCol = osType;
            ResultsCol = results;
            WhyCol = why;
            ScOksCol = scOks;
        }

        public ControlSqlObjects()
        {
        }

        #endregion
    }
}
