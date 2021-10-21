using System;
using System.Collections.Generic;

#nullable disable

namespace GreenLife.Models
{
    public partial class ProductionCategory
    {
        public ProductionCategory()
        {
            ProductionProducts = new HashSet<ProductionProduct>();
        }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public virtual ICollection<ProductionProduct> ProductionProducts { get; set; }
    }
}
