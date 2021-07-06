using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkTrackingLib.Interfaces;

namespace WorkTrackingLib.Models
{
    abstract public class BaseTableModel : ISelectedItem
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
