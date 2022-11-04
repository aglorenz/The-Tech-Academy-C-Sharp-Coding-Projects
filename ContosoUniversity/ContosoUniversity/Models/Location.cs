using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ContosoUniversity.Models
{
    public class Location
    {
        public int LocationID { get; set; }

        [Display(Name = "Location Name")]
        [Required(ErrorMessage = "The Location Name is required")]
        public String LocationName { get; set; }

        [Required(ErrorMessage = "The Address is required")]
        public string Address { get; set; }
        
        [Required(ErrorMessage = "The State is required")]
        public String State { get; set; }

        [Display(Name = "Zip Code")]
        [Required(ErrorMessage = "The Zip Code is required")]
        [RegularExpression(@"^\d{5}(-\d{4})?$", ErrorMessage = "Invalid Zip")]
        public int ZipCode { get; set; }

        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "The Email Address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone Number is required")]
        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public string PhoneNumber { get; set; }

    }
}