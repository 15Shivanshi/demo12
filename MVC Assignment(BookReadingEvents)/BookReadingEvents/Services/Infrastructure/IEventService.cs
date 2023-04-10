using Services.Models;
using System.Collections.Generic;

namespace Services.Infrastructure
{
    //INTERFACE FOR EVENT SERVICE
    public interface IEventService
    {
        int Create(BookReadingEvent bookReadingEvent);
        void AddNewComment(int eventID, string commentByUser, string comment);
        BookReadingEvent GetDetails(int id);
        bool Exists(int id);
        bool UpdateDetails(BookReadingEvent bookReadingEvent);
        List<ShowEvent> GetAllEvents(string EmailID="");
        List<ShowEvent> GetMyEvents(string EmailID);
        List<ShowEvent> GetEventsInvitedTo(string myEmailID);
    }
}
