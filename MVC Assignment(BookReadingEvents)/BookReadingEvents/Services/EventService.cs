using DataAccess;
using DataAccess.DatabaseModels;
using DataAccess.HelperModels;
using Services.Exceptions;
using Services.Infrastructure;
using Services.Models;
using System.Collections.Generic;

namespace Services
{
    public class EventService : IEventService
    {
        //TO CREATE A NEW BOOK READING EVENT
        public int Create(BookReadingEvent bookReadingEvent)
        {
            BookReadingEventDataAccess bookReadingEventDataAccess = new BookReadingEventDataAccess();
        
            BookReadingEventEntity bookReadingEventEntity = AutomapperWebProfile.Mapper.Map<BookReadingEvent, BookReadingEventEntity>(bookReadingEvent);

            int id = bookReadingEventDataAccess.Create(bookReadingEventEntity); // return EventID

            if (id == 0)
            {
                throw new ServiceException("Database error, please try again later");
            }
            else
            {
                return id;
            }
        }

        //TO ADD A NEW COMMENT TO AN EVENT 
        public void AddNewComment(int eventID, string emailID, string comment)
        {
            BookReadingEventDataAccess bookReadingEventDataAccess = new BookReadingEventDataAccess();

            UserDataAccess userDataAccess = new UserDataAccess();

            if (userDataAccess.Find(emailID) == null)
            {
                throw new ServiceException("Please Login to Add Comment.");
            }
            else
            {
                CommentEntity newCommentEntity = new CommentEntity
                {
                    //NewCommentEntity.CommentId will be auto gentrated
                    EventID = eventID,
                    UserEmailID = emailID,
                    Description = comment
                };

                if (!bookReadingEventDataAccess.AddNewComment(newCommentEntity))
                {
                    throw new ServiceException(" Event not found ERROR !! Comment cound not be added! ");
                }
            }

        }

        //TO UPDATE DETAILS OF A BOOK READING EVENT
        public bool UpdateDetails(BookReadingEvent bookReadingEvent)
        {
            BookReadingEventDataAccess bookReadingEventDataAccess = new BookReadingEventDataAccess();
            
            BookReadingEventEntity bookReadingEventEntity = AutomapperWebProfile.Mapper.Map<BookReadingEvent, BookReadingEventEntity>(bookReadingEvent);

            bool updated = bookReadingEventDataAccess.Update(bookReadingEventEntity);

            if (!updated)
            {
                //throw new ServiceException("Database error, please try again later");
                return false;
            }
            else
            {
                return true;
            }
        }

        // TO FETCH AND RETURN DETAILS OF A BOOK READING EVENT
        public BookReadingEvent GetDetails(int id)
        {
            BookReadingEventDataAccess bookReadingEventDataAccess = new BookReadingEventDataAccess();

            BookReadingEventEntity bookReadingEventEntity = bookReadingEventDataAccess.Find(id);

            if (bookReadingEventEntity == null)
            {
                //throw new ServiceException("Event Not found");
                return null;
            }

            BookReadingEvent bookReadingEvent =AutomapperWebProfile.Mapper.Map<BookReadingEventEntity, BookReadingEvent>(bookReadingEventEntity);
            
            return bookReadingEvent;
        }

        //TO GET ALL THE PUBLIC BOOK READING EVENTS OF TYPE List<ShowEvent>
        public List<ShowEvent> GetAllEvents(string EmailID = "")
        {
            BookReadingEventDataAccess bookReadingEventDataAccess = new BookReadingEventDataAccess();

            List<ShowEvent> eventList = new List<ShowEvent>();

            var getList = bookReadingEventDataAccess.GetAllBookReadingEvents(EmailID);
            
            eventList = AutomapperWebProfile.Mapper.Map<List<ShowEventEntity>, List<ShowEvent>>(getList);
            
            return eventList ;
        }

        // TO GET ALL THE USER CREATED BOOK READING EVENTS OF TYPE List<ShowEvent>
        public List<ShowEvent> GetMyEvents(string EmailID)
        {
            if (EmailID.Equals("") || EmailID == null)
            {
                return null;
            }

            BookReadingEventDataAccess bookReadingEventDataAccess = new BookReadingEventDataAccess();

            List<ShowEvent> eventList = new List<ShowEvent>();
            var getList = bookReadingEventDataAccess.GetMyBookReadingEvents(EmailID);

            eventList = AutomapperWebProfile.Mapper.Map<List<ShowEventEntity>, List<ShowEvent>>(getList);

            return eventList;
        }

        // TO GET ALL THE USER INVITED TO BOOK READING EVENTS OF TYPE List<ShowEvent>
        public List<ShowEvent> GetEventsInvitedTo(string EmailID)
        {
            if(EmailID.Equals("") || EmailID==null)
            {
                return null;
            }

            BookReadingEventDataAccess bookReadingEventDataAccess = new BookReadingEventDataAccess();

            List<ShowEvent> eventList = new List<ShowEvent>();

            var getList = bookReadingEventDataAccess.GetEventsInvitedTo(EmailID);

            eventList = AutomapperWebProfile.Mapper.Map<List<ShowEventEntity>, List<ShowEvent>>(getList);

            return eventList;
        }

        //TO CHECK IF USER EXISTS OR NOT
        public bool Exists(int EventID)
        {
            BookReadingEventDataAccess bookReadingEventDataAccess = new BookReadingEventDataAccess();

            if(bookReadingEventDataAccess.Find(EventID)!=null)
            {
                return true;
            }

            return false;
        }
    }
}
