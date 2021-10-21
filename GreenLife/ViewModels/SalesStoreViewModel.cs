using GreenLife.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GreenLife.ViewModels
{
    public class SalesStoreViewModel
    {
        public SalesStoreViewModel()
        {
            ProductionStocks = new HashSet<ProductionStock>();
            SalesOrders = new HashSet<SalesOrder>();
            SalesStaffs = new HashSet<SalesStaff>();
            citieslist = new List<storecities>();
        }
        public List<storecities> citieslist { get; set; } // for list
        public string citiesname { get; set; }// create  for to get  list of cityname from cities table 
        public int StoreId { get; set; }
        [Required]
        public string StoreName { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Street { get; set; }

        public int? Cityid { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string ZipCode { get; set; }

        //public virtual City City { get; set; }
        public virtual ICollection<ProductionStock> ProductionStocks { get; set; }
        public virtual ICollection<SalesOrder> SalesOrders { get; set; }
        public virtual ICollection<SalesStaff> SalesStaffs { get; set; }
    }
    public class storecities
    {
        public int Id { get; set; }
        public string CityName { get; set; }
    }
}
