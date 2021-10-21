using GreenLife.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreenLife.ViewModels.Helpers
{
    public class Item
    {
        public ProductCheckOut product { get; set; }
        public int Quantity { get; set; }
    }
    public class ProductCheckOut
    {

        // public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string BrandName { get; set; }
        public string CategoryName { get; set; }
        public decimal ListPrice { get; set; }
        public String Photo { get; set; }

    }
}
