using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GreenLife.ViewModels.City
{
    public class CityCreateViewModel
    {
        public int cityid { get; set; }
        [Required]
        public string cityname { get; set; }

    }
}
