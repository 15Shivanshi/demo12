using Infrastructure.Models;
using System;
using System.Collections.Generic;

namespace Infrastructure.Sevices
{
    public interface IEventService
    {
        int Create(BookReadingEvent bookReadingEvent);
        void AddNewComment(int eventID, string commentByUser, string comment);
        BookReadingEvent GetDetails(int id);
        bool UpdateDetails(BookReadingEvent bookReadingEvent);
        List<Tuple<string, int>> GetAllEvents(string EmailID="");
        List<Tuple<string, int>> GetMyEvents(string EmailID);
        List<Tuple<string, int>> GetEventsInvitedTo(string myEmailID);
    }
}
