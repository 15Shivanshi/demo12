using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookReadingEvents.ViewModels
{
    public class AddCommentToEvent
    {
        [Required]
        public int EventID { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Comment { get; set; }
    }
}