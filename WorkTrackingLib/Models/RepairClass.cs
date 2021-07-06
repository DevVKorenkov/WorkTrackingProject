using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace WorkTrackingLib.Models
{
    public class RepairClass : PropertyChangeClass, ICloneable
    {
        private int id = 0;
        public int Id
        {
            get { return id; }
            set { id = value; OnPropertyChanged(nameof(Id)); }
        }

        private int deviceId;
        public int DeviceId
        {
            get { return deviceId; }
            set { deviceId = value; OnPropertyChanged(nameof(DeviceId)); }
        }

        private DateTime? date;
        [Display(Name = "Дата")]
        public DateTime? Date
        {
            get { return date; }
            set { date = value; OnPropertyChanged(nameof(Date)); }
        }

        private string status = string.Empty;
        [Display(Name = "Статус")]
        public string Status
        {
            get { return status; }
            set { status = value; OnPropertyChanged(nameof(Status)); }
        }

        private string osName = string.Empty;
        [Display(Name = "Наименование")]
        public string OsName
        {
            get { return osName; }
            set { osName = value; OnPropertyChanged(nameof(OsName)); }
        }

        private string model = string.Empty;
        [Display(Name = "Модель")]
        public string Model
        {
            get { return model; }
            set { model = value; OnPropertyChanged(nameof(Model)); }
        }

        private string sNumber = string.Empty;
        [Display(Name = "Серийный номер")]
        public string SNumber
        {
            get { return sNumber; }
            set { sNumber = value; OnPropertyChanged(nameof(SNumber)); }
        }

        private string invNumber = string.Empty;
        [Display(Name = "Инв.номер")]
        public string InvNumber
        {
            get { return invNumber; }
            set { invNumber = value; OnPropertyChanged(nameof(InvNumber)); }
        }

        private string diagNumber = string.Empty;
        [Display(Name = "Основание")]
        public string DiagNumber
        {
            get { return diagNumber; }
            set { diagNumber = value; OnPropertyChanged(nameof(DiagNumber)); }
        }

        private string scOks = string.Empty;
        [Display(Name = "СЦ ОКС")]
        public string ScOks
        {
            get => scOks;
            set { scOks = value; OnPropertyChanged(nameof(ScOks)); }
        }

        private string kaProvider = string.Empty;
        [Display(Name = "КА Поставщик")]
        public string KaProvider
        {
            get { return kaProvider; }
            set { kaProvider = value; OnPropertyChanged(nameof(KaProvider)); }
        }

        private string kaRepair = string.Empty;
        [Display(Name = "КА Ремонт")]
        public string KaRepair
        {
            get { return kaRepair; }
            set { kaRepair = value; OnPropertyChanged(nameof(KaRepair)); }
        }

        private string handedOver = string.Empty;
        [Display(Name = "Передал")]
        public string HandedOver
        {
            get { return handedOver; }
            set { handedOver = value; OnPropertyChanged(nameof(HandedOver)); }
        }

        private string defect = string.Empty;
        [Display(Name = "Неисправность")]
        public string Defect
        {
            get { return defect; }
            set { defect = value; OnPropertyChanged(nameof(Defect)); }
        }

        private DateTime? shipmentDate;
        [Display(Name = "Дата отгрузки")]
        public DateTime? ShipmentDate
        {
            get { return shipmentDate; }
            set { shipmentDate = value; OnPropertyChanged(nameof(ShipmentDate)); ChangeDaysOfRepair(); }
        }

        private int daysOfRepair;
        [Display(Name = "Дней в ремонте")]
        public int DaysOfRepair
        {
            get { return daysOfRepair; }
            set { daysOfRepair = value; OnPropertyChanged(nameof(DaysOfRepair)); }
        }

        private DateTime? returnFromRepair;
        [Display(Name = "Вернулось из ремонта")]
        public DateTime? ReturnFromRepair
        {
            get { return returnFromRepair; }
            set { returnFromRepair = value; OnPropertyChanged(nameof(ReturnFromRepair)); ChangeDaysOfRepair(); }
        }

        private string providerOrder = string.Empty;
        [Display(Name = "Номер заявки поставщика СЦ")]
        public string ProviderOrder
        {
            get { return providerOrder; }
            set { providerOrder = value; OnPropertyChanged(nameof(ProviderOrder)); }
        }

        private string repairBill = string.Empty;
        [Display(Name = "Согласованная стоимость ремонта")]
        public string RepairBill
        {
            get { return repairBill; }
            set { repairBill = value; OnPropertyChanged(nameof(RepairBill)); }
        }

        private string warrantyBasis = string.Empty;
        [Display(Name = "Основание гарантии")]
        public string WarrantyBasis
        {
            get { return warrantyBasis; }
            set { warrantyBasis = value; OnPropertyChanged(nameof(WarrantyBasis)); }
        }

        private DateTime? startWarranty;
        [Display(Name = "Дата начала гарантии")]
        public DateTime? StartWarranty
        {
            get { return startWarranty; }
            set { startWarranty = value; OnPropertyChanged(nameof(StartWarranty)); }
        }

        private string warranty = string.Empty;
        [Display(Name = "Тип ремонта")]
        public string Warranty
        {
            get { return warranty; }
            set { warranty = value; OnPropertyChanged(nameof(Warranty)); }
        }

        private bool haveAccumulator;
        [Display(Name = "Аккумулятор")]
        public bool HaveAccumulator
        {
            get => haveAccumulator;
            set { haveAccumulator = value; OnPropertyChanged(nameof(HaveAccumulator)); }
        }

        private bool haveFlashMemory;
        [Display(Name = "Карта памяти")]
        public bool HaveFlashMemory
        {
            get => haveFlashMemory;
            set { haveFlashMemory = value; OnPropertyChanged(nameof(HaveFlashMemory)); }
        }

        private bool haveHandBelt;
        [Display(Name = "Ремешок")]
        public bool HaveHandBelt
        {
            get => haveHandBelt;
            set { haveHandBelt = value; OnPropertyChanged(nameof(HaveHandBelt)); }
        }

        private bool haveStylus;
        [Display(Name = "Стилус")]
        public bool HaveStylus
        {
            get => haveStylus;
            set { haveStylus = value; OnPropertyChanged(nameof(HaveStylus)); }
        }

        private void ChangeDaysOfRepair()
        {
            if (ShipmentDate != null && ReturnFromRepair == null)
            {
                TimeSpan time = DateTime.Now - Convert.ToDateTime(ShipmentDate);

                DaysOfRepair = time.Days;
            }

            if (ShipmentDate != null && ReturnFromRepair != null)
            {
                TimeSpan time = Convert.ToDateTime(ReturnFromRepair) - Convert.ToDateTime(ShipmentDate);

                DaysOfRepair = time.Days;
            }
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
