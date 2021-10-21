using GreenLife.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreenLife.ViewModels
{
    public class CityViewModel
    {
        public CityViewModel()
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
