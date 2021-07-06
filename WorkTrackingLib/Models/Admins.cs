using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkTrackingLib.Models
{
    public class Admins : ICloneable
    {
        /// <summary>
        /// Property ID of the Employee in the database 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Property of the employee's name in the database
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Access level property
        /// </summary>
        public int Access { get; set; }

        /// <summary>
        /// Property SC OKS system administrator
        /// </summary>
        public string ScOKS { get; set; }

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
