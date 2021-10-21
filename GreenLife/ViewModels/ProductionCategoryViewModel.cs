using GreenLife.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GreenLife.ViewModels
{
    public class ProductionCategoryViewModel
    {
        public ProductionCategoryViewModel()
        {
            ProductionProducts = new HashSet<ProductionProduct>();
        }

        public int CategoryId { get; set; }
        [Required]
        public string CategoryName { get; set; }

        public virtual ICollection<ProductionProduct> ProductionProducts { get; set; }
    }
}
