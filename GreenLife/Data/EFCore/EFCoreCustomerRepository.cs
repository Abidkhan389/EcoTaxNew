using GreenLife.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreenLife.Data.EFCore
{
    public class EFCoreCustomerRepository : EfCoreRepository<SalesCustomer, GreenLifeFinalContext>
    {
        public EFCoreCustomerRepository(GreenLifeFinalContext context) : base(context)
        {

        }
    }
}
