using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GreenLife.ViewModels.ContactUs
{
    public class ContactUsViewModel
    {
        public int ID { get; set; }
        //[Required]
        public string firstname { get; set; }
        public string lastname { get; set; }
        [Required]
        //[RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$",
          // ErrorMessage = "Invalid Email Format")]
        public string email { get; set; }
        public string address { get; set; }
       // [Required]
        public string phonenumber { get; set; }
       // [Required]
        public string message { get; set; }

    }
}
