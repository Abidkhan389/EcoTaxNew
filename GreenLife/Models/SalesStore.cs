using System;
using System.Collections.Generic;

#nullable disable

namespace GreenLife.Models
{
    public partial class SalesStore
    {
        public SalesStore()
        {
            ProductionStocks = new HashSet<ProductionStock>();
            SalesOrders = new HashSet<SalesOrder>();
            SalesStaffs = new HashSet<SalesStaff>();
        }

        public int StoreId { get; set; }
        public string StoreName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Street { get; set; }
        public int? Cityid { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }

        public virtual City City { get; set; }
        public virtual ICollection<ProductionStock> ProductionStocks { get; set; }
        public virtual ICollection<SalesOrder> SalesOrders { get; set; }
        public virtual ICollection<SalesStaff> SalesStaffs { get; set; }
    }
}
