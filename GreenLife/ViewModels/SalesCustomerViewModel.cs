using GreenLife.Models;
using GreenLife.ViewModels.City;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GreenLife.ViewModels
{
    public class SalesCustomerViewModel
    {
        public SalesCustomerViewModel()
        {
            SalesOrders = new HashSet<SalesOrder>();
            citieslist = new List<cities>();
            //paymentlist = new List<mobilepayment>();
        }
        public List<cities> citieslist { get; set; } // for list
        //public List<mobilepayment> paymentlist { get; set; } // for list

        public string citiesname { get; set; }// create  for to get  list of cityname from cities table

        public string productName { get; set; }
        public decimal productprice { get; set; }
        public string productphoto { get; set; }

        public double subtotal { get; set; }
        public string Shipping { get; set; }
        public double Total { get; set; }
        public int CustomerId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]

        public string LastName { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$",
            ErrorMessage = "Invalid Email Format")]
        public string Email { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        public int? Cityid { get; set; }
        [Required]
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string orderstatus { get; set; }
        [Required]
        public int emailopt { get; set; }
        [Required]
        public string TransactionTID { get; set; }
        [Required]
        public PaymentMethod? paymentmode { get; set; }
        public virtual CityCreateViewModel city { get; set; }
        public virtual ICollection<SalesOrder> SalesOrders { get; set; }
    }
    public class cities
    {
        public int Id { get; set; }
        public string CityName { get; set; }
    }
    //public class mobilepayment
    //{
    //    public int Id { get; set; }
    //    public string paymentName { get; set; }
    //}
}
