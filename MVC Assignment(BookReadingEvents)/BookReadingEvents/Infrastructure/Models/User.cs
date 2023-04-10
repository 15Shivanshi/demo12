using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Models
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
        public string Password { get; set; }

        [Required]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        public virtual ICollection<BookReadingEvent> BookReadingEvents { get; set; }
    }
}
