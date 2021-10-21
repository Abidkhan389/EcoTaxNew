using GreenLife.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreenLife.ViewModels
{
    public class ProductionStockViewModel
    {
        public int StoreId { get; set; }
        public int ProductId { get; set; }
        public int? Quantity { get; set; }

        public virtual ProductionProduct Product { get; set; }
        public virtual SalesStore Store { get; set; }
    }
}
