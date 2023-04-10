using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models
{
    public class Comment
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

        public virtual BookReadingEvent BookReadingEvent { get; set; }

        public Comment() { }
        public Comment(string emailID, string description)
        {
            UserEmailID = emailID;
            Description = description;
        }

        //TEMPORARY CONSTRUCTOR.....TO BE DELETED
        public Comment(int id, string emailID, string description)
        {
            CommentId = id;
            UserEmailID = emailID;
            Description = description;
        }
    }
}
