using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkTrackingLib;
using WorkTrackingLib.Models;

namespace WorkTracking_Server.Context
{
    /// <summary>
    /// Database tables data context 
    /// </summary>
    public class DataContext : DbContext
    {
        Dictionary<string, SqlConnectionString> connections;

        public DbSet<NewWrite> ComplitedWorks { get; set; }

        public DbSet<RepairClass> Repairs { get; set; }

        public DbSet<Devices> Devices { get; set; }

        public DbSet<Admins> Admins { get; set; }

        public DbSet<Osp> Osp { get; set; }

        public DbSet<OsType> OsType { get; set; }

        public DbSet<Results> Results { get; set; }

        public DbSet<Why> Why { get; set; }

        public DbSet<ScOks> ScOks { get; set; }

        public DbSet<RepairsStatus> RepairsStatuses { get; set; }

        public DataContext()
        {
            connections = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, SqlConnectionString>>(System.IO.File.ReadAllText(@"wwwroot\SqlConnectionString.json"));

            try
            {
                Database.EnsureCreated();
            }
            catch
            {

            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optBuilder)
        {
            optBuilder.UseSqlServer(connections["test"].ConnectionString);
        }
    }
}
