namespace TaskMVC.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using TaskMVC.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<TaskMVC.DAL.AppContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(TaskMVC.DAL.AppContext context)
        {
            var managers = new List<Manager>
            {
                new Manager { FirstMidName = "Carson",   LastName = "Alexander"},
                new Manager { FirstMidName = "Meredith", LastName = "Alonso"},
                new Manager { FirstMidName = "Arturo",   LastName = "Anand"},
                new Manager { FirstMidName = "Gytis",    LastName = "Barzdukas"},
                new Manager { FirstMidName = "Yan",      LastName = "Li"},
                new Manager { FirstMidName = "Peggy",    LastName = "Justice"},
                new Manager { FirstMidName = "Laura",    LastName = "Norman"},
                new Manager { FirstMidName = "Nino",     LastName = "Olivetto"}
            };
            managers.ForEach(s => context.Managers.AddOrUpdate(p => p.LastName, s));
            context.SaveChanges();           

            var salesInfo = new List<SaleInfo>
            {
                new SaleInfo {
                    ManagerID = managers.Single(s => s.LastName == "Alexander").ManagerID,
                    ClientName = "Bob",
                    ProductName = "car",
                    ProductCost = "1500",
                    DateOfSale = DateTime.Parse("2010-09-01")
                },
                 new SaleInfo {
                    ManagerID = managers.Single(s => s.LastName == "Alexander").ManagerID,
                    ClientName = "Rick",
                    ProductName = "car",
                    ProductCost = "1000",
                    DateOfSale = DateTime.Parse("2011-09-01")
                 },
                 new SaleInfo {
                    ManagerID = managers.Single(s => s.LastName == "Alexander").ManagerID,
                    ClientName = "Jeck",
                    ProductName = "pen",
                    ProductCost = "100",
                    DateOfSale = DateTime.Parse("2011-07-01")
                 },
                 new SaleInfo {
                     ManagerID = managers.Single(s => s.LastName == "Alonso").ManagerID,
                    ClientName = "Rick",
                    ProductName = "car",
                    ProductCost = "1000",
                    DateOfSale = DateTime.Parse("2011-08-01")
                 },
                 new SaleInfo {
                    ManagerID = managers.Single(s => s.LastName == "Alonso").ManagerID,
                    ClientName = "Richard",
                    ProductName = "car",
                    ProductCost = "1000",
                    DateOfSale = DateTime.Parse("2012-09-01")
                 },
                 new SaleInfo {
                    ManagerID = managers.Single(s => s.LastName == "Alonso").ManagerID,
                    ClientName = "Rick",
                    ProductName = "car",
                    ProductCost = "1400",
                    DateOfSale = DateTime.Parse("2014-09-01")
                 },
                 new SaleInfo {
                    ManagerID = managers.Single(s => s.LastName == "Anand").ManagerID,
                    ClientName = "Rick",
                    ProductName = "car",
                    ProductCost = "1000",
                    DateOfSale = DateTime.Parse("2012-09-01")
                 },
                 new SaleInfo {
                    ManagerID = managers.Single(s => s.LastName == "Anand").ManagerID,
                    ClientName = "Jim",
                    ProductName = "phone",
                    ProductCost = "320",
                    DateOfSale = DateTime.Parse("2013-09-05")
                 },
                new SaleInfo {
                    ManagerID = managers.Single(s => s.LastName == "Barzdukas").ManagerID,
                    ClientName = "Ron",
                    ProductName = "car",
                    ProductCost = "1000",
                    DateOfSale = DateTime.Parse("2014-09-01")
                 },
                 new SaleInfo {
                    ManagerID = managers.Single(s => s.LastName == "Li").ManagerID,
                    ClientName = "Harry",
                    ProductName = "pencil",
                    ProductCost = "120",
                    DateOfSale = DateTime.Parse("2010-09-01")
                 },
                 new SaleInfo {
                    ManagerID = managers.Single(s => s.LastName == "Justice").ManagerID,
                    ClientName = "Rick",
                    ProductName = "boat",
                    ProductCost = "2000",
                    DateOfSale = DateTime.Parse("2009-09-03")
                 }
            };

            foreach (SaleInfo e in salesInfo)
            {
                var salesInDataBase = context.SalesInfo.Where(
                    s =>s.Manager.ManagerID == e.ManagerID).SingleOrDefault();
                if (salesInDataBase == null)
                {
                    context.SalesInfo.Add(e);
                }
            }
            context.SaveChanges();
        }
    }
}
