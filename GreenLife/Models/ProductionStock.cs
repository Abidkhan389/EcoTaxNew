using System;
using System.Collections.Generic;

#nullable disable

namespace GreenLife.Models
{
    public partial class ProductionStock
    {
        public int StoreId { get; set; }
        public int ProductId { get; set; }
        public int? Quantity { get; set; }

        public virtual ProductionProduct Product { get; set; }
        public virtual SalesStore Store { get; set; }
    }
}
