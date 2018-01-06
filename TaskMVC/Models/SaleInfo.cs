using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskMVC.Models
{
    public class SaleInfo
    {
        public int SaleInfoID { get; set; }
        public int ManagerID { get; set; }
        public string ClientName { get; set; }
        public string ProductName { get; set; }
        public string ProductCost { get; set; }
        public DateTime DateOfSale { get; set; }

        public virtual Manager Manager { get; set; }
    }
}