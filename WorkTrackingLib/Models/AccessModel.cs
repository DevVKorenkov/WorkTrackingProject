using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkTrackingLib.Models
{
    public class AccessModel : PropertyChangeClass
    {
        #region Properties

        /// <summary>
        /// Property ID of the Employee in the database
        /// </summary>
        public int Id { get; set; }

        private string name;
        /// <summary>
        /// Property of the employee's name in the database 
        /// </summary>
        public string Name 
        {
            get { return name; }
            set { name = value; OnPropertyChanged(nameof(Name)); }
        }

        private string scOks;
        /// <summary>
        /// Property of the employee's name in the database 
        /// </summary>
        public string ScOks
        {
            get { return scOks; }
            set { scOks = value; OnPropertyChanged(nameof(ScOks)); }
        }

        private int access;
        /// <summary>
        /// Access level property 
        /// </summary>
        public int Access 
        {
            get { return access; }
            set { access = value; OnPropertyChanged(nameof(Access)); }
        }

        private bool visibilityControlSql;
        /// <summary>
        /// The visibility property of the database control button 
        /// </summary>
        public bool VisibilityControlSql
        {
            get { return visibilityControlSql; }
            set { visibilityControlSql = value; OnPropertyChanged(nameof(VisibilityControlSql)); }
        }

        private bool visibilityControl;
        /// <summary>
        /// The visibility property of the database control button 
        /// </summary>
        public bool VisibilityControl
        {
            get { return visibilityControl; }
            set { visibilityControl = value; OnPropertyChanged(nameof(VisibilityControl)); }
        }
        
        private bool visibilityRepairs;
        /// <summary>
        /// The visibility property of the database control button 
        /// </summary>
        public bool VisibilityRepairs
        {
            get { return visibilityRepairs; }
            set { visibilityRepairs = value; OnPropertyChanged(nameof(VisibilityRepairs)); }
        }
        
        private bool visibilityDevices;
        /// <summary>
        /// The visibility property of the database control button 
        /// </summary>
        public bool VisibilityDevices
        {
            get { return visibilityDevices; }
            set { visibilityDevices = value; OnPropertyChanged(nameof(VisibilityDevices)); }
        }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Constructor that accepts the received parameters 
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="Name"></param>
        /// <param name="Access"></param>
        public AccessModel(int Id, string Name, int Access)
        {
            this.Id = Id;
            this.Name = Name;
            this.Access = Access;
        }

        public AccessModel()
        {
        }

        public AccessModel(string Name)
        {
            this.Name = Name;
        }

        #endregion
    }
}
