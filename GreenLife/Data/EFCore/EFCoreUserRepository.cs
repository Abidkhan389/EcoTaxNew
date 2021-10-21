using GreenLife.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreenLife.Data.EFCore
{
    public class EFCoreUserRepository : EfCoreRepository<AspNetRoles, GreenLifeFinalContext>
    {
        public EFCoreUserRepository(GreenLifeFinalContext context) : base(context)
        {

        }
    }   
}
