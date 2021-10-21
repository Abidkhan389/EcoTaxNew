using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GreenLife.Utilities
{
    public class ValidEmailDomainAttiributes:ValidationAttribute
    {
        private readonly string allowedDoamin;

        public ValidEmailDomainAttiributes(string allowedDoamin )
        {
            this.allowedDoamin = allowedDoamin;
        }

        public override bool IsValid(object value)
        {
            string[]  strings= value.ToString().Split("@");
            return strings[1].ToUpper() == allowedDoamin.ToUpper();
        }
    }
}
