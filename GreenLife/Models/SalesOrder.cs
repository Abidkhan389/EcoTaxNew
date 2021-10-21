using System;
using System.Collections.Generic;

#nullable disable

namespace GreenLife.Models
{
    public partial class SalesOrder
    {
        public SalesOrder()
        {
            SalesOrderItems = new HashSet<SalesOrderItem>();
        }

        public int OrderId { get; set; }
        public int? CustomerId { get; set; }
        public byte OrderStatus { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime RequiredDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public int StoreId { get; set; }
        public int StaffId { get; set; }

        public virtual SalesCustomer Customer { get; set; }
        public virtual SalesStaff Staff { get; set; }
        public virtual SalesStore Store { get; set; }
        public virtual ICollection<SalesOrderItem> SalesOrderItems { get; set; }
    }
}
