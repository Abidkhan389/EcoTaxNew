using GreenLife.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GreenLife.ViewModels
{
    public class SalesOrderViewModel
    {
        public SalesOrderViewModel()
        {
            customerslist = new List<customer>(); 
            storeslist = new List<Store>();
            stafflist = new List<staff>();

        }
        public List<customer> customerslist { get; set; } // for customer list
        public string customerfirstname { get; set; }// create  for to get  list of customername from customer table
        public string customerlastname { get; set; }// create  for to get  list of customername from customer table

        public List<Store> storeslist { get; set; } // for store list
        public string storename { get; set; }// create  for to get  list of storename from store table

        public List<staff> stafflist { get; set; } // for staff list
        public string staffirstname { get; set; }// create  for to get  list of staffname from satff table
        public string staflastname { get; set; }// create  for to get  list of staffname from satff table


        public int OrderId { get; set; }
        public int? CustomerId { get; set; }
        [Required]
        public byte OrderStatus { get; set; }
        [Required]
        public Boolean choseorderstatus { get; set; }
        [Required]
        public DateTime OrderDate { get; set; }
        [Required]
        public DateTime RequiredDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        [Required]
        public int StoreId { get; set; }
        [Required]
        public int StaffId { get; set; }
    }
    public class customer
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
    public class Store
    {
        public int StoreId { get; set; }
        [Required]
        public string StoreName { get; set; }
    }
    public class staff
    {
        public int StaffId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
