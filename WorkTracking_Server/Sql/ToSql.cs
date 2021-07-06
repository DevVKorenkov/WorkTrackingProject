using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkTracking_Server.Context;
using WorkTrackingLib;
using WorkTrackingLib.Interfaces;
using WorkTrackingLib.Models;

namespace WorkTracking_Server.Sql
{
    public class ToSql
    {
        /// <summary>
        /// Database data context
        /// </summary>
        private DataContext dataContext;

        public ToSql(DataContext context)
        {
            dataContext = context;
        }

        /// <summary>
        /// Adds a new work object to the database 
        /// </summary>
        /// <param name="newWork"></param>
        /// <returns></returns>
        public async Task<bool> AddWork(NewWrite newWork)
        {
            try
            {
                dataContext.ComplitedWorks.Add(newWork);

                await dataContext.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Adds a new repair object to the database 
        /// </summary>
        /// <param name="newRepair"></param>
        /// <returns></returns>
        public async Task<bool> AddRepair(RepairClass newRepair)
        {
            try
            {
                dataContext.Repairs.Add(newRepair);

                await dataContext.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Adds a new device object to the database 
        /// </summary>
        /// <param name="newDevice"></param>
        /// <returns></returns>
        public async Task<bool> AddDevice(Devices newDevice)
        {
            try
            {
                if (!dataContext.Devices.Select(x => x.InvNumber).Contains(newDevice.InvNumber))
                {
                    dataContext.Devices.Add(newDevice);

                    await dataContext.SaveChangesAsync();

                    return true;
                }
                else
                    return false;

            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Adds a new user object to the database 
        /// </summary>
        /// <param name="admin"></param>
        /// <returns></returns>
        public async Task<bool> AddNewUser(Admins admin)
        {
            try
            {
                dataContext.Admins.Add(admin);

                await dataContext.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Adds a new item object to the database 
        /// </summary>
        /// <param name="item"></param>
        /// <param name="table"></param>
        /// <returns></returns>
        public async Task<bool> AddNewItem(object item, string table)
        {
            try
            {
                switch (table)
                {
                    case "ScOks":
                        dataContext.ScOks.Add(JsonConvert.DeserializeObject<ScOks>(item.ToString()));
                        break;
                    case "Osp":
                        dataContext.Osp.Add(JsonConvert.DeserializeObject<Osp>(item.ToString()));
                        break;
                    case "OsType":
                        dataContext.OsType.Add(JsonConvert.DeserializeObject<OsType>(item.ToString()));
                        break;
                    case "Results":
                        dataContext.Results.Add(JsonConvert.DeserializeObject<Results>(item.ToString()));
                        break;
                    case "Why":
                        dataContext.Why.Add(JsonConvert.DeserializeObject<Why>(item.ToString()));
                        break;
                    case "RepairStatus":
                        dataContext.RepairsStatuses.Add(JsonConvert.DeserializeObject<RepairsStatus>(item.ToString()));
                        break;
                }

                await dataContext.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Removes a work object from the database
        /// </summary>
        /// <param name="item"></param>
        /// <param name="table"></param>
        /// <returns></returns>
        public async Task<bool> DelItem(object item, string table)
        {
            try
            {
                switch (table)
                {
                    case "User":
                        dataContext.Admins.Remove(JsonConvert.DeserializeObject<Admins>(item.ToString()));
                        break;
                    case "ScOks":
                        dataContext.ScOks.Remove(JsonConvert.DeserializeObject<ScOks>(item.ToString()));
                        break;
                    case "Osp":
                        dataContext.Osp.Remove(JsonConvert.DeserializeObject<Osp>(item.ToString()));
                        break;
                    case "OsType":
                        dataContext.OsType.Remove(JsonConvert.DeserializeObject<OsType>(item.ToString()));
                        break;
                    case "Results":
                        dataContext.Results.Remove(JsonConvert.DeserializeObject<Results>(item.ToString()));
                        break;
                    case "Why":
                        dataContext.Why.Remove(JsonConvert.DeserializeObject<Why>(item.ToString()));
                        break;
                    case "RepairStatus":
                        dataContext.RepairsStatuses.Remove(JsonConvert.DeserializeObject<RepairsStatus>(item.ToString()));
                        break;
                }

                await dataContext.SaveChangesAsync();

                return true;
            }
            catch (DbUpdateException e)
            {
                Console.WriteLine(e.InnerException.Message);
                return false;
            }
        }

        /// <summary>
        /// Changes the object of work
        /// </summary>
        /// <param name="mutableWork"></param>
        /// <returns></returns>
        public async Task<bool> ChangeWork(NewWrite mutableWork)
        {
            try
            {
                #region _
                //var tempWork = dataContext.ComplitedWorks.Where(x => x.Id == mutableWork.Id).FirstOrDefault();

                //foreach (var t in tempWork.GetType().GetProperties())
                //{
                //    foreach (var m in mutableWork.GetType().GetProperties())
                //    {
                //        if (t.Name == m.Name)
                //        {
                //            t.SetValue(tempWork, m.GetValue(mutableWork));

                //            continue;
                //        }
                //    }
                //}
                #endregion

                dataContext.ComplitedWorks.Update(mutableWork);

                await dataContext.SaveChangesAsync();

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException.Message);
                return false;
            }
        }

        /// <summary>
        /// Changes the object of repair
        /// </summary>
        /// <param name="mutableRepair"></param>
        /// <returns></returns>
        public async Task<bool> ChangeRepair(RepairClass mutableRepair)
        {
            try
            {
                dataContext.Repairs.Update(mutableRepair);

                #region _
                //var tempWork = dataContext.Repairs.Where(x => x.Id == mutableRepair.Id).FirstOrDefault();

                //foreach (var t in tempWork.GetType().GetProperties())
                //{
                //    foreach (var m in mutableRepair.GetType().GetProperties())
                //    {
                //        if (t.Name == m.Name)
                //        {
                //            t.SetValue(tempWork, m.GetValue(mutableRepair));

                //            continue;
                //        }
                //    }
                //}
                #endregion

                await dataContext.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Changes the object of device
        /// </summary>
        /// <param name="mutableDevice"></param>
        /// <returns></returns>
        public async Task<bool> ChangeDevice(Devices mutableDevice)
        {
            bool tempAnswer = true;

            try
            {
                var temp = dataContext.Devices.Where(x => x.Id == mutableDevice.Id).FirstOrDefault();

                temp = mutableDevice;

                foreach (var t in dataContext.Devices)
                {                   
                    if (t.InvNumber != mutableDevice.InvNumber && t.Id != mutableDevice.Id)
                    {
                        tempAnswer = true;
                    }
                    else if(t.InvNumber == mutableDevice.InvNumber && t.Id == mutableDevice.Id)
                    {
                        tempAnswer = true;
                    }
                    else if(t.InvNumber == mutableDevice.InvNumber && t.Id != mutableDevice.Id)
                    {
                        tempAnswer = false;

                        break;
                    }
                }

                if (tempAnswer)
                {
                    await dataContext.SaveChangesAsync();
                }

                return tempAnswer;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Changes the object of user
        /// </summary>
        /// <param name="mutableUser"></param>
        /// <returns></returns>
        public async Task<bool> ChangeUser(Admins mutableUser)
        {
            try
            {
                dataContext.Admins.Update(mutableUser);

                await dataContext.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Changes the object of item
        /// </summary>
        /// <param name="item"></param>
        /// <param name="table"></param>
        /// <returns></returns>
        public async Task<bool> ChangeItem(object item, string table)
        {
            try
            {
                switch (table)
                {
                    case "ScOks":
                        dataContext.ScOks.Update(JsonConvert.DeserializeObject<ScOks>(item.ToString()));
                        break;
                    case "Osp":
                        dataContext.Osp.Update(JsonConvert.DeserializeObject<Osp>(item.ToString()));
                        break;
                    case "OsType":
                        dataContext.OsType.Update(JsonConvert.DeserializeObject<OsType>(item.ToString()));
                        break;
                    case "Results":
                        dataContext.Results.Update(JsonConvert.DeserializeObject<Results>(item.ToString()));
                        break;
                    case "Why":
                        dataContext.Why.Update(JsonConvert.DeserializeObject<Why>(item.ToString()));
                        break;
                    case "RepairStatus":
                        dataContext.RepairsStatuses.Update(JsonConvert.DeserializeObject<RepairsStatus>(item.ToString()));
                        break;
                }

                await dataContext.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
