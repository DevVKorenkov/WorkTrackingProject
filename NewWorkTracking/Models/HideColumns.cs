using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkTrackingLib;

namespace NewWorkTracking.Models
{
    /// <summary>
    /// Data Model representing DataGrid columns (Table in the tab all repairs) 
    /// </summary>
    class HideColumns : PropertyChangeClass
    {
        private bool date;
        public bool Date
        {
            get { return date; }
            set { date = value; OnPropertyChanged(nameof(Date)); }
        }

        private bool status;
        public bool Status
        {
            get { return status; }
            set { status = value; OnPropertyChanged(nameof(Status)); }
        }

        private bool osName;
        public bool OsName
        {
            get { return osName; }
            set { osName = value; OnPropertyChanged(nameof(OsName)); }
        }

        private bool model;
        public bool Model
        {
            get { return model; }
            set { model = value; OnPropertyChanged(nameof(Model)); }
        }

        private bool sNumber;
        public bool SNumber
        {
            get { return sNumber; }
            set { sNumber = value; OnPropertyChanged(nameof(SNumber)); }
        }

        private bool invNumber;
        public bool InvNumber
        {
            get { return invNumber; }
            set { invNumber = value; OnPropertyChanged(nameof(InvNumber)); }
        }

        private bool scOks;
        public bool ScOks
        {
            get { return scOks; }
            set { scOks = value; OnPropertyChanged(nameof(ScOks)); }
        }

        private bool diagNumber;
        public bool DiagNumber
        {
            get { return diagNumber; }
            set { diagNumber = value; OnPropertyChanged(nameof(DiagNumber)); }
        }

        private bool kaProvider;
        public bool KaProvider
        {
            get { return kaProvider; }
            set { kaProvider = value; OnPropertyChanged(nameof(KaProvider)); }
        }

        private bool kaRepair;
        public bool KaRepair
        {
            get { return kaRepair; }
            set { kaRepair = value; OnPropertyChanged(nameof(KaRepair)); }
        }

        private bool handedOver;
        public bool HandedOver
        {
            get { return handedOver; }
            set { handedOver = value; OnPropertyChanged(nameof(HandedOver)); }
        }

        private bool defect;
        public bool Defect
        {
            get { return defect; }
            set { defect = value; OnPropertyChanged(nameof(Defect)); }
        }

        private bool shipmentDate;
        public bool ShipmentDate
        {
            get { return shipmentDate; }
            set { shipmentDate = value; OnPropertyChanged(nameof(ShipmentDate)); }
        }

        private bool daysOfRepair;
        public bool DaysOfRepair
        {
            get { return daysOfRepair; }
            set { daysOfRepair = value; OnPropertyChanged(nameof(DaysOfRepair)); }
        }

        private bool returnFromRepair;
        public bool ReturnFromRepair
        {
            get { return returnFromRepair; }
            set { returnFromRepair = value; OnPropertyChanged(nameof(ReturnFromRepair)); }
        }

        private bool providerOrder;
        public bool ProviderOrder
        {
            get { return providerOrder; }
            set { providerOrder = value; OnPropertyChanged(nameof(ProviderOrder)); }
        }

        private bool repairBill;
        public bool RepairBill
        {
            get { return repairBill; }
            set { repairBill = value; OnPropertyChanged(nameof(RepairBill)); }
        }

        private bool warrantyBasis;
        public bool WarrantyBasis
        {
            get { return warrantyBasis; }
            set { warrantyBasis = value; OnPropertyChanged(nameof(WarrantyBasis)); }
        }

        private bool startWarranty;
        public bool StartWarranty
        {
            get { return startWarranty; }
            set { startWarranty = value; OnPropertyChanged(nameof(StartWarranty)); }
        }

        private bool warranty;
        public bool Warranty
        {
            get { return warranty; }
            set { warranty = value; OnPropertyChanged(nameof(Warranty)); }
        }
    }
}
