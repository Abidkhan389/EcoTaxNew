using GreenLife.Data;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

#nullable disable

namespace GreenLife.Models
{
    public partial class ProductionProduct
    {
        public ProductionProduct()
        {
            ProductionStocks = new HashSet<ProductionStock>();
            SalesOrderItems = new HashSet<SalesOrderItem>();
        }
       // public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int BrandId { get; set; }
        public int CategoryId { get; set; }
        public short ModelYear { get; set; }
        public decimal ListPrice { get; set; }
        public String Photo { get; set; }
        public decimal actual_product_price { get; set; }

        public virtual ProductionBrand Brand { get; set; }
        public virtual ProductionCategory Category { get; set; }
        public virtual ICollection<ProductionStock> ProductionStocks { get; set; }
        public virtual ICollection<SalesOrderItem> SalesOrderItems { get; set; }
    }
}
