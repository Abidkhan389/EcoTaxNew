using GreenLife.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GreenLife.ViewModels
{
    public class SalesStaffViewModel
    {
        public SalesStaffViewModel()
        {
            InverseManager = new HashSet<SalesStaff>();
            SalesOrders = new HashSet<SalesOrder>();
            storelist = new List<stores>();
        }
        public List<stores> storelist { get; set; } // for list
        public string storesName { get; set; } //// create  for to get  list of storename from staff table 
       
        public int StaffId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]

        public string LastName { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$",
            ErrorMessage = "Invalid Email Format")]
        public string Email { get; set; }
        [Required]

        public string Phone { get; set; }
        [Required]

        public byte Active { get; set; }
        [Required]

        public int StoreId { get; set; }
        [Required]

        public int? ManagerId { get; set; }
        public Boolean choseactivetype { get; set; }

        public virtual SalesStaff Manager { get; set; }
        public virtual SalesStore Store { get; set; }
        public virtual ICollection<SalesStaff> InverseManager { get; set; }
        public virtual ICollection<SalesOrder> SalesOrders { get; set; }
    }
    public class stores
    {
        public int Id { get; set; }
        public string StoreName { get; set; }
    }
}
