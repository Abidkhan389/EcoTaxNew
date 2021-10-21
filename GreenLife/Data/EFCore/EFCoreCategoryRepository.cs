using GreenLife.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreenLife.Data.EFCore
{
    public class EFCoreCategoryRepository:EfCoreRepository<ProductionCategory, GreenLifeFinalContext>
    {
        public EFCoreCategoryRepository(GreenLifeFinalContext context) : base(context)
        {

        }
    }

}
