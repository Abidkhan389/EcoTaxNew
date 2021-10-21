using System;
using System.Collections.Generic;

#nullable disable

namespace GreenLife.Models
{
    public partial class SalesStaff
    {
        public SalesStaff()
        {
            InverseManager = new HashSet<SalesStaff>();
            SalesOrders = new HashSet<SalesOrder>();
        }

        public int StaffId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public byte Active { get; set; }
        public int StoreId { get; set; }
        public int? ManagerId { get; set; }

        public virtual SalesStaff Manager { get; set; }
        public virtual SalesStore Store { get; set; }
        public virtual ICollection<SalesStaff> InverseManager { get; set; }
        public virtual ICollection<SalesOrder> SalesOrders { get; set; }
    }
}
