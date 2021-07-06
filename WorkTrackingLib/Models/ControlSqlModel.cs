using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkTrackingLib.Interfaces;

namespace WorkTrackingLib
{
    public class ControlSqlModel : ISelectedItem
    {
        #region Properties

        /// <summary>
        /// Property of the identification number in the database table
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name property
        /// </summary>
        public string Name { get; set; }

        #endregion

        #region Constructors 

        /// <summary>
        /// Constructor for getting data from the database
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="Name"></param>
        public ControlSqlModel(int Id, string Name)
        {
            this.Id = Id;
            this.Name = Name;
        }

        public ControlSqlModel()
        {
        }

        #endregion

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
