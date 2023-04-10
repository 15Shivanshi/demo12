using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookReadingEvents.ViewModels
{
    public class UserLogin
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email ID")]
        public string EmailID { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(5,ErrorMessage ="The Field Password must have a minimum length of 5.")]
        public string Password { get; set; }
    }
}