using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using WorkTracking_Server.Context;
using WorkTrackingLib;
using WorkTrackingLib.Models;

namespace WorkTracking_Server.Sql
{
    public class FromSql
    {
        /// <summary>
        /// Database data context
        /// </summary>
        private DataContext dataContext;

        public FromSql(DataContext context)
        {
            dataContext = context;
        }

        /// <summary>
        /// Returns a list of work objects
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<NewWrite> GetWorks()
        {
            return new ObservableCollection<NewWrite>(dataContext.ComplitedWorks.OrderByDescending(x => x.Date));
        }

        /// <summary>
        /// Returns a list of work objects by username
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public ObservableCollection<NewWrite> GetWorks(string userName)
        {
            return new ObservableCollection<NewWrite>(dataContext.ComplitedWorks.Where(x => x.Who == userName && x.Date <= DateTime.Today && x.Date >= DateTime.Now.AddMonths(-1)).OrderByDescending(x => x.Date));
        }

        /// <summary>
        /// Returns a List of device objects 
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<Devices> GetDevices()
        {
            ObservableCollection<Devices> tempDev = new ObservableCollection<Devices>(dataContext.Devices);
            ObservableCollection<RepairClass> tempRep = new ObservableCollection<RepairClass>(GetRepairs());

            foreach (var d in tempDev)
            {
                d.Repairs = new ObservableCollection<RepairClass>(tempRep.Where(x => x.DeviceId == d.Id));
            }

            return new ObservableCollection<Devices>(tempDev.OrderBy(x => x.InvNumber));
        }

        /// <summary>
        /// Returns a List of repair objects 
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<RepairClass> GetRepairs()
        {
            #region _
            //foreach (var r in dataContext.Repairs)
            //{
            //    foreach (PropertyInfo p in r.GetType().GetProperties())
            //    {
            //        if (p.PropertyType == typeof(DateTime))
            //        {
            //            if (p.GetValue(r) == null)
            //            {
            //                p.SetValue(r, null);
            //            }
            //        }
            //    }
            //}
            #endregion

            return new ObservableCollection<RepairClass>(dataContext.Repairs);
        }

        /// <summary>
        /// The method gets a list of all users contained in the database users table
        /// </summary>
        /// <returns></returns>
        public AccessModel GetAdmins(string userName)
        {
            var tempUser = dataContext.Admins.Where(x => x.Name == userName).FirstOrDefault();

            if (tempUser != null)
            {
                return new AccessModel() { Id = tempUser.Id, Name = tempUser.Name, ScOks = tempUser.ScOKS, Access = tempUser.Access };
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Forms lists for adding and filtering
        /// </summary>
        /// <returns></returns>
        public ComboboxDataSource GetComboboxes()
        {            
            ComboboxDataSource comboboxData = new ComboboxDataSource(
                dataContext.Admins.Select(x => x.Name).ToList(),
                dataContext.Osp.OrderBy(x => x.Name).ToList(),
                dataContext.OsType.OrderBy(x => x.Name).ToList(),
                dataContext.Results.OrderBy(x => x.Name).ToList(),
                dataContext.Why.OrderBy(x => x.Name).ToList(),
                dataContext.ScOks.OrderBy(x => x.Name).ToList(),
                dataContext.RepairsStatuses.OrderBy(x => x.Name).ToList(),
                new List<Admins>(dataContext.Admins));

            return comboboxData;
        }
    }
}
