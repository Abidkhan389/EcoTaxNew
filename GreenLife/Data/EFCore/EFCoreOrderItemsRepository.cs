using GreenLife.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreenLife.Data.EFCore
{
    public class EFCoreOrderItemsRepository : EfCoreRepository<SalesOrderItem, GreenLifeFinalContext>
    {
        public EFCoreOrderItemsRepository(GreenLifeFinalContext context) : base(context)
        {

        }
    }
}
