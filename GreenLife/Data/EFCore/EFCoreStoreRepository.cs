using GreenLife.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreenLife.Data.EFCore
{
    public class EFCoreStoreRepository : EfCoreRepository<SalesStore, GreenLifeFinalContext>
    {
        public EFCoreStoreRepository(GreenLifeFinalContext context) : base(context)
        {

        }
    }
}
