using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreenLife.ViewModels
{
    public class ApplicationUser: IdentityUser
    {
        private string _City;
        public string City
        {
            get { return this._City; }
            set { this._City = value; }
        }

    }
}
