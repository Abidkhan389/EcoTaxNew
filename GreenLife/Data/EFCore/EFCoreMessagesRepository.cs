using GreenLife.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreenLife.Data.EFCore
{
    public class EFCoreMessagesRepository : EfCoreRepository<Messages, GreenLifeFinalContext>
    {
        public EFCoreMessagesRepository(GreenLifeFinalContext context) : base(context)
        {

        }
    }
}
