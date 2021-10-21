using GreenLife.Models;
using GreenLife.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreenLife.Data.EFCore
{
    public class EfCoreProductRepository : EfCoreRepository<ProductionProduct, GreenLifeFinalContext>
    {
        public EfCoreProductRepository(GreenLifeFinalContext context) : base(context)
        {

        }
    }
}
