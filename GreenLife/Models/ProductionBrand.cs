using System;
using System.Collections.Generic;

#nullable disable

namespace GreenLife.Models
{
    public partial class ProductionBrand
    {
        public ProductionBrand()
        {
            ProductionProducts = new HashSet<ProductionProduct>();
        }

        public int BrandId { get; set; }
        public string BrandName { get; set; }

        public virtual ICollection<ProductionProduct> ProductionProducts { get; set; }
    }
}
