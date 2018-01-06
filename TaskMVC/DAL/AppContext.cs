using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaskMVC.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace TaskMVC.DAL
{
    public class AppContext : DbContext
    {
        public DbSet<Manager> Managers { get; set; }
        public DbSet<SaleInfo> SalesInfo { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}