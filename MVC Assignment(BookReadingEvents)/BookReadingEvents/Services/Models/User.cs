using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Services.Models
{
    public class User
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email ID")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string EmailID { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [RegularExpression("(?=(.*[a-z])*)(?=(.*[\\W])+).{5,}", ErrorMessage = " 1. Password Lenght must be more than 5. <br> " +
            "2. Include atleast one special character (!@#%&*).")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        public virtual ICollection<BookReadingEvent> BookReadingEvents { get; set; }
    }
}
