using GreenLife.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreenLife.Data.EFCore
{
    public class EFCoreStaffRepository: EfCoreRepository<SalesStaff, GreenLifeFinalContext>
    {
        public EFCoreStaffRepository(GreenLifeFinalContext context) : base(context)
        {

        }
    }
}

