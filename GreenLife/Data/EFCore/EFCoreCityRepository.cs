using GreenLife.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreenLife.Data.EFCore
{
    public class EFCoreCityRepository : EfCoreRepository<City, GreenLifeFinalContext>
    {
        public EFCoreCityRepository(GreenLifeFinalContext context) : base(context)
        {

        }
    }
   
}
