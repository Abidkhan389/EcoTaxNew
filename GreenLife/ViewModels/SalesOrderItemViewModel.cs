using GreenLife.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GreenLife.ViewModels
{
    public class SalesOrderItemViewModel
    {
        public ProductionProduct product { get; set; }
        [Required]
        public int OrderId { get; set; }
        [Required]
        public int ItemId { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public decimal ListPrice { get; set; }
        public decimal Discount { get; set; }

        public virtual SalesOrder Order { get; set; }
        public virtual ProductionProduct Product { get; set; }
    }
    
}
