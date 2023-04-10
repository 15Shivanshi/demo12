using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Models
{
    public enum EventType { Public, Private };

    public class BookReadingEvent
    {
        [Key]
        [Display(Name = "Event ID")]
        public int EventID { get; set; }

        [Display(Name = "Host")]
        [ForeignKey("User")]
        [Required(AllowEmptyStrings = false)]
        public string HostEmailID { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Title { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string Location { get; set; }

        [Display(Name = "Start Time")]
        public DateTime StartTime { get; set; } //DropDown time from 00-23:00

        [Range(0, 4, ErrorMessage = "Duration must be between 0-4 hours.")]
        public int Duration { get; set; } //hours

        [StringLength(50, ErrorMessage = "Description cannot be longer than 50 characters.")]
        public string Description { get; set; }

        [StringLength(500, ErrorMessage = "Other details cannot be longer than 500 characters.")]
        [Display(Name = "Other Details")]
        public string OtherDetails { get; set; }

        [Display(Name = "Type of Event")]
        public EventType Type { get; set; } = EventType.Public; //default public 

        [Display(Name = "Invited Users Emails")]
        public string InvitedUsers { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual User User { get; set; }

    }
}
