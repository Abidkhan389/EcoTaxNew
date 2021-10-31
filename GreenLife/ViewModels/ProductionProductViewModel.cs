using GreenLife.Data;
using GreenLife.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GreenLife.ViewModels
{
    public class ProductionProductViewModel
    {
        public ProductionProductViewModel()
        {
            ProductionStocks = new HashSet<ProductionStock>();
            SalesOrderItems = new HashSet<SalesOrderItem>();
            categorylist = new List<category>();
            brandlist = new List<brand>();
        }
        public List<category>  categorylist{ get; set; }
        public List<brand> brandlist { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        public int numberofquantity { get; set; }
        public int ProductId { get; set; }

        [Required]
        [MaxLength(80, ErrorMessage = "Name can not exceed 80 characters")]
       
        public string ProductName { get; set; }
        [Required]
        public int BrandId { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public string CateogryName { get; set; } //create  for to get  list of categoryname from category table 
        public string BrandName { get; set; } //create for to get  list of brandname from brand table
        //[DataType(DataType.Date)]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{MM/dd/yyyy}")]
        public short ModelYear { get; set; }
        [Required]
        public decimal ListPrice { get; set; }
        public decimal actual_product_price { get; set; }

        public string Productphoto { get; set; }
        public IFormFile? Photo { get; set; }
       // public IFormFile ExistingPhtopath { get; set; }
        public virtual ProductionBrand Brand { get; set; }
        public virtual ProductionCategory Category { get; set; }
        public virtual ICollection<ProductionStock> ProductionStocks { get; set; }
        public virtual ICollection<SalesOrderItem> SalesOrderItems { get; set; }
    }
    public class category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
    public class brand
    {
        public int BrandId { get; set; }
        public string BrandName { get; set; }
    }
}
