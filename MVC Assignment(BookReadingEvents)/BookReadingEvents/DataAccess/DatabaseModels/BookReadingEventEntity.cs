using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.DatabaseModels
{
    public enum EventType { Public, Private };

    public class BookReadingEventEntity
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
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        [Display(Name = "Start Time")]
        public TimeSpan StartTime { get; set; }

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
        [RegularExpression("(((\\w+@\\w+\\.[A-Za-z]{2,4}\\.?[A-Za-z]{0,4}),?)*)", ErrorMessage = "Please specify (,) seperated valid EmailIDs.")]
        public string InvitedUsers { get; set; }

        public virtual ICollection<CommentEntity> Comments { get; set; }

        public virtual UserEntity User { get; set; }

    }
}
