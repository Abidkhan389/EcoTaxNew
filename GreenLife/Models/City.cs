using System;
using System.Collections.Generic;

#nullable disable

namespace GreenLife.Models
{
    public partial class City
    {
        public City()
        {
            SalesCustomers = new HashSet<SalesCustomer>();
            SalesStores = new HashSet<SalesStore>();
        }

        public int Id { get; set; }
        public string CityName { get; set; }

        public virtual ICollection<SalesCustomer> SalesCustomers { get; set; }
        public virtual ICollection<SalesStore> SalesStores { get; set; }
    }
}
