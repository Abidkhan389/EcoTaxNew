using GreenLife.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreenLife.Data.EFCore
{
    public class EFCoreBrandRepository: EfCoreRepository<ProductionBrand, GreenLifeFinalContext>
    {
        public EFCoreBrandRepository(GreenLifeFinalContext context) : base(context)
        {

        }
    }

}
