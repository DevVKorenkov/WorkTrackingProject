using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkTrackingLib.Models
{
    /// <summary>
    /// The class provides lists for the Combox Client program 
    /// </summary>
    public class ComboboxDataSource : PropertyChangeClass
    {
        #region List properties

        private List<Admins> accesses;
        public List<Admins> Accesses
        {
            get => accesses;
            set { accesses = value; OnPropertyChanged(nameof(Accesses)); }
        }

        private List<string> adminList;
        public List<string> AdminsList
        {
            get => adminList;
            set { adminList = value; OnPropertyChanged(nameof(AdminsList)); }
        }

        private List<Osp> ospList;
        public List<Osp> OspList
        {
            get => ospList;
            set { ospList = value; OnPropertyChanged(nameof(OspList)); }
        }

        private List<OsType> osTypeList;
        public List<OsType> OsTypeList
        {
            get => osTypeList;
            set { osTypeList = value; OnPropertyChanged(nameof(OsTypeList)); }
        }

        private List<Results> resultList;
        public List<Results> ResultsList
        {
            get => resultList;
            set { resultList = value; OnPropertyChanged(nameof(ResultsList)); }
        }

        private List<Why> whyList;
        public List<Why> WhyList
        {
            get => whyList;
            set { whyList = value; OnPropertyChanged(nameof(WhyList)); }
        }

        private List<ScOks> scOks;
        public List<ScOks> ScOks
        {
            get => scOks;
            set { scOks = value; OnPropertyChanged(nameof(ScOks)); }
        }

        private List<RepairsStatus> repairStatus;
        public List<RepairsStatus> RepairStatus
        {
            get => repairStatus;
            set { repairStatus = value; OnPropertyChanged(nameof(RepairStatus)); }
        }

        #endregion

        #region Constructors

        public ComboboxDataSource
            (
            List<string> adminsList,
            List<Osp> ospList,
            List<OsType> osTypeList,
            List<Results> resultsList,
            List<Why> whyList,
            List<ScOks> scOks,
            List<RepairsStatus> repairStatus,
            List<Admins> accesses
            )
        {
            this.AdminsList = adminsList;
            this.OspList = ospList;
            this.OsTypeList = osTypeList;
            this.ResultsList = resultsList;
            this.WhyList = whyList;
            this.ScOks = scOks;
            this.RepairStatus = repairStatus;
            this.Accesses = accesses;
        }

        public ComboboxDataSource()
        {
        }

        #endregion
    }
}
