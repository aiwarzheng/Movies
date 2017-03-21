using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Domain.Entities
{
    public class ShippingDetails
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter your name")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter the first address line")]
        [Display(Name = "Line 1")]
        public string Line1 { get; set; }
        [Display(Name = "Line 2")]
        public string Line2 { get; set; }
        [Display(Name = "Line 3")]
        public string Line3 { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter a city name")]
        public string City { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter a state name")]
        public string State { get; set; }

        public string Zip { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter a country name")]
        public string Country { get; set; }

        public bool GiftWrap { get; set; }
    }
}
