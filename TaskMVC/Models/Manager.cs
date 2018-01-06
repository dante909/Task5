using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskMVC.Models
{
    public class Manager
    {
        public int ManagerID { get; set; }
        public string LastName { get; set; }
        public string FirstMidName { get; set; }

        public virtual ICollection<SaleInfo> SalesInfo { get; set; }
    }
}