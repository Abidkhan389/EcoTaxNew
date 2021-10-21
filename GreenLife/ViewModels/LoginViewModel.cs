using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GreenLife.ViewModels
{ 
    public class LoginViewModel
    {
        #region Vaiables
        private string _Email;
        private string _password;
        private bool _RememberMe;
        #endregion
        #region Properties
        [Required]
        //[EmailAddress]
        public string Email
        {
            get { return this._Email; }
            set { this._Email = value; }
        }

        [Required]
        [DataType(DataType.Password)]
        public string Password
        {
            get { return this._password; }
            set { this._password = value; }
        }
        [Display(Name = "Remember Me")]
        public bool RememberMe
        {
            get { return this._RememberMe; }
            set { this._RememberMe = value; }
        }
        #endregion
    }
}
