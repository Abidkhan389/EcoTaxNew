using System;
using System.Collections.Generic;

#nullable disable

namespace GreenLife.Models
{
    public partial class SalesCustomer
    {
        public SalesCustomer()
        {
            SalesOrders = new HashSet<SalesOrder>();
        }

        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Street { get; set; }
        public int? Cityid { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string orderstatus { get; set; }
        public int emailopt { get; set; }
        public string TransactionTID { get; set; }
        public string paymentmode { get; set; }

        public virtual City City { get; set; }
        public virtual ICollection<SalesOrder> SalesOrders { get; set; }
    }
}
