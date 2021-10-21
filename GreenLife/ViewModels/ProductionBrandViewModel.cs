using GreenLife.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GreenLife.ViewModels
{
    public class ProductionBrandViewModel
    {
        public ProductionBrandViewModel()
        {
            ProductionProducts = new HashSet<ProductionProduct>();
        }

        public int BrandId { get; set; }
        [Required]
        public string BrandName { get; set; }

        public virtual ICollection<ProductionProduct> ProductionProducts { get; set; }
    }
}
