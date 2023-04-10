using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.DatabaseModels
{   
    public class CommentEntity
    {
        [Key]
        public int CommentId { get; set; }

        [Required]
        public int EventID { get; set; }

        [Required(AllowEmptyStrings = false)]
        //user email id
        public string UserEmailID { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Description { get; set; }

        public virtual BookReadingEventEntity BookReadingEvents { get; set; }
    }
}
