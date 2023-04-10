using DataAccess.DatabaseModels;
using DataAccess.HelperModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace DataAccess
{
    public class BookReadingEventDataAccess
    {
        private EventDbContext context = new EventDbContext();

        //IT CREATES NEW EVENT
        public int Create(BookReadingEventEntity bookReadingEventEntity)
        {
            try
            {
                context.BookReadingEvents.Add(bookReadingEventEntity);
                context.SaveChanges();
                return bookReadingEventEntity.EventID;
            }
            catch (SqlException e)
            {
                System.Diagnostics.Debug.WriteLine("DATABASE ERROR : " + e.Message);
                return 0;
            }
        }

        // ADDS A COMMENT TO AN EXISTING EVENT
        public bool AddNewComment(CommentEntity commentEntity)
        {
            try {
                context.Comments.Add(commentEntity);
                context.SaveChanges();
                return true;
            }
            catch (SqlException e)
            {
                System.Diagnostics.Debug.WriteLine("DATABASE ERROR : " + e.Message);
                return false;
            }
        }

        // TO UPDATE AN EXISTING BOOK READING EVENT
        public bool Update(BookReadingEventEntity bookReadingEventEntity)
        {
            try {
                BookReadingEventEntity getEventData = context.BookReadingEvents.Find(bookReadingEventEntity.EventID);
                getEventData.Title = bookReadingEventEntity.Title;
                getEventData.Date = bookReadingEventEntity.Date.Date;
                getEventData.Location = bookReadingEventEntity.Location;
                getEventData.StartTime = bookReadingEventEntity.StartTime;
                getEventData.Duration = bookReadingEventEntity.Duration;
                getEventData.Description = bookReadingEventEntity.Description;
                getEventData.OtherDetails = bookReadingEventEntity.OtherDetails;
                getEventData.Type = bookReadingEventEntity.Type;
                getEventData.InvitedUsers = bookReadingEventEntity.InvitedUsers;
                context.SaveChanges();

                return true;
            }
            catch (SqlException e)
            {
                System.Diagnostics.Debug.WriteLine("DATABASE ERROR : " + e.Message);
                return false;
            }
        }

        // RETURNS ONE BOOK EVENT IF EXISTS ELSE RETURNS NULL
        public BookReadingEventEntity Find(int id)
        {
            return context.BookReadingEvents.Find(id);
        }


        //RETURN All PUBLIC BOOK EVENTS OF TYPE List<ShowEventEntity>
        public List<ShowEventEntity> GetAllBookReadingEvents(string EmailID="")
        {
            List<ShowEventEntity> showEventList = new List<ShowEventEntity>();

            if (EmailID.Equals("myadmin@bookevents.com"))
            {
                var list = (from x in context.BookReadingEvents
                            select x).ToList();
                for (int i = 0; i < list.Count(); i++)
                {
                    showEventList.Add(new ShowEventEntity { EventTitle = list[i].Title, EventID = list[i].EventID, EventDate = list[i].Date });

                }
            }
            else
            {
                var list = (from x in context.BookReadingEvents.ToList()
                            where x.Type == EventType.Public
                            select x).ToList();
                for (int i = 0; i < list.Count(); i++)
                {
                    showEventList.Add(new ShowEventEntity { EventTitle = list[i].Title, EventID = list[i].EventID, EventDate = list[i].Date });

                }
            }
            return showEventList;
        }

        //RETURN USER CREATED EVENTS OF TYPE List<ShowEventEntity>
        public List<ShowEventEntity> GetMyBookReadingEvents(string EmailID)
        {
            List<ShowEventEntity> showEventList = new List<ShowEventEntity>();

            if (EmailID.Equals("") || EmailID == null)
            {
                return null;
            }

            var list = (from x in context.BookReadingEvents.ToList()
                        where x.HostEmailID.Equals(EmailID)
                        select x).ToList();
            for (int i = 0; i < list.Count(); i++)
            {
                showEventList.Add(new ShowEventEntity() { EventTitle = list[i].Title,
                                                          EventID = list[i].EventID,
                                                          EventDate = list[i].Date});
            }

            return showEventList;
        }

        //RETURN USER SPECIFIC EVENTS INVITED TO OF TYPE List<ShowEventEntity>
        public List<ShowEventEntity>  GetEventsInvitedTo(string EmailID)
        {
            List<ShowEventEntity> showEventList = new List<ShowEventEntity>();

            if (EmailID.Equals("") || EmailID == null)
            {
                return null;
            }

            var list = (from x in context.BookReadingEvents.ToList()
                        where !String.IsNullOrEmpty(x.InvitedUsers) && x.InvitedUsers.Contains(EmailID)
                        select x).ToList();

            for(int i = 0; i < list.Count(); i++)
            {
                showEventList.Add(new ShowEventEntity()
                {
                    EventTitle = list[i].Title,
                    EventID = list[i].EventID,
                    EventDate = list[i].Date
                });
            }

            return showEventList;

        }

    }
}
