using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.DatabaseModels
{
    public class UserEntity
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

        public virtual ICollection<BookReadingEventEntity> BookReadingEvents { get; set; }
    }
}
