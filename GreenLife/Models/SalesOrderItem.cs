using System;
using System.Collections.Generic;

#nullable disable

namespace GreenLife.Models
{
    public partial class SalesOrderItem
    {
        public int OrderId { get; set; }
        public int ItemId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal ListPrice { get; set; }
        public decimal Discount { get; set; }

        public virtual SalesOrder Order { get; set; }
        public virtual ProductionProduct Product { get; set; }
    }
}
