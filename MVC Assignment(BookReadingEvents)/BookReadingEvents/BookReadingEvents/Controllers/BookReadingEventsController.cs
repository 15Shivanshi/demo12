using BookReadingEvents.Filters;
using BookReadingEvents.ViewModels;
using Services;
using Services.Exceptions;
using Services.Infrastructure;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace BookReadingEvents.Controllers
{
    public class BookReadingEventsController : Controller
    {
        // GET: BookReadingEvents
        public ActionResult Index()
        {
            IEventService eventService = new ServiceFactory().GetEventService();
            List<ShowEvent> eventList;
           
            //initializing values to session variables
            if (Session["UserID"] == null)
            {
                Session["UserID"] = "";
                Session["LoggedIn"] = false;
            }

            eventList = eventService.GetAllEvents(Session["UserID"].ToString());

            return View(eventList);
        }


        // GET: BookReadingEvents/MyEvents
        [UserAuthenticationFilter]
        [UserAuthorizationFilter]
        public ActionResult MyEvents()
        {
            IEventService eventService = new ServiceFactory().GetEventService();
            List<ShowEvent> eventList;
            
            //get all the user created book reading events list
            eventList = eventService.GetMyEvents(Session["UserID"].ToString());
            eventList = (from item in eventList
                    orderby item.EventDate
                    select item).ToList();

            return View(eventList);
        }


        // GET: BookReadingEvents/EventsInvitedTo
        [UserAuthenticationFilter]
        [UserAuthorizationFilter]
        public ActionResult EventsInvitedTo()
        {
            IEventService eventService = new ServiceFactory().GetEventService();
            List<ShowEvent> eventList;

            //Get all the user-invited-to events list
            eventList = eventService.GetEventsInvitedTo(Session["UserID"].ToString());

            return View(eventList);
        }


        // GET: BookReadingEvents/Details/5
        public ActionResult Details(int id, string error = "")
        {
            ModelState.AddModelError("", error);
            IEventService eventService = new ServiceFactory().GetEventService();

            BookReadingEvent bookReadinEventModel = eventService.GetDetails(id);

            //return "Not found" if event with entered id doesn`t exist
            if (bookReadinEventModel == null)
            {
                return HttpNotFound();
            }
            
            //If event is private and user is not authorized return error
            if ((bookReadinEventModel.Type == Services.Models.EventType.Private) &&
                (Convert.ToBoolean(Session["LoggedIn"]) == false ||
                    !(Session["UserID"].ToString().Equals("admin@gmail.com") ||
                      bookReadinEventModel.HostEmailID.Equals(Session["UserID"].ToString()))))
            {
                //checking if user is invited to the private event
                if (bookReadinEventModel.InvitedUsers != null)
                {
                    if (bookReadinEventModel.InvitedUsers.Contains(Session["UserID"].ToString()))
                    { return View(bookReadinEventModel); }

                    
                }
                return View("NotAuthorized");
            }
            
            return View(bookReadinEventModel);
        }

        [HttpPost, ActionName("Details")]
        [UserAuthenticationFilter]
        [UserAuthorizationFilter]
        public ActionResult AddCommentDetails(AddCommentToEvent addCommentToEvent)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "invalid input");
                return RedirectToAction("Details", new { @id = addCommentToEvent.EventID });
            }

            IEventService eventService = new ServiceFactory().GetEventService();

            try
            {
                eventService.AddNewComment(addCommentToEvent.EventID, Session["UserID"].ToString(), addCommentToEvent.Comment);
            }
            catch (ServiceException)
            {
                ModelState.AddModelError("", "Comment Could Not Be Added");
            }

            //redirects to details page; after adding a new comment
            return RedirectToAction("Details", new { @id = addCommentToEvent.EventID });
        }

        // GET: BookReadingEvents/Create
        [UserAuthenticationFilter]
        public ActionResult Create()
        {
            return View();
        }

        // POST: BookReadingEvents/Create
        [HttpPost]
        [UserAuthenticationFilter]
        public ActionResult Create(CreateBookReadingEvent createBookReadingEvent)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            IEventService eventService = new ServiceFactory().GetEventService();

            //create new book reading event
            BookReadingEvent newBookReadingEvent = new BookReadingEvent
            {

                HostEmailID = Session["UserID"].ToString(),
                Title = createBookReadingEvent.Title,
                Date = createBookReadingEvent.Date,
                Location = createBookReadingEvent.Location,
                StartTime = new TimeSpan(createBookReadingEvent.Hours,createBookReadingEvent.Minutes,0),
                Duration = createBookReadingEvent.Duration,
                Description = createBookReadingEvent.Description,
                OtherDetails = createBookReadingEvent.OtherDetails,
                Type = (Services.Models.EventType)createBookReadingEvent.Type,
                InvitedUsers = createBookReadingEvent.InvitedUsers
            };

            try
            {
                int newlyCreatedEventId = eventService.Create(newBookReadingEvent);

                return RedirectToAction("Details", new { @id = newlyCreatedEventId });
            }
            catch (ServiceException)
            {
                ModelState.AddModelError("", "Event Could Not Be Created");
                return View();
            }
        }

        // GET: BookReadingEvents/Edit/5
        [UserAuthenticationFilter]
        [UserAuthorizationFilter]
        public ActionResult Edit(int id)
        {
            IEventService eventService = new ServiceFactory().GetEventService();
            BookReadingEvent bookReadingEvent = eventService.GetDetails(id);
           
            if(bookReadingEvent == null)
            {
                return HttpNotFound();
            }
            // if logged-in but not admin or host of event => cannot edit
            // if authorized and not admin => cannot edit past events.
            if (Session["UserID"].ToString().Equals("admin@gmail.com"))
            { 
                return View(bookReadingEvent);
            }
            else if(bookReadingEvent.HostEmailID.Equals(Session["UserID"]))
            {
                if(bookReadingEvent.Date < DateTime.Now)
                    return RedirectToAction("Details", new { @id=id, @error=" You Cannot Edit a Past Event. "});
                else
                    return View(bookReadingEvent);
            }


            //return not authorized
            return View("NotAuthorized");
        }

        // POST: BookReadingEvents/Edit/id
        [HttpPost, ActionName("Edit")]
        [UserAuthenticationFilter]
        [UserAuthorizationFilter]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int id, CreateBookReadingEvent updateBookReadingEvent)
        {
            IEventService eventService = new ServiceFactory().GetEventService();

            //return not found if event doesn`t exist
            if(!eventService.Exists(id))
            {
                return HttpNotFound();
            }

            BookReadingEvent updatedBookReadingEvent = new BookReadingEvent
            {
                EventID = id,
                Title = updateBookReadingEvent.Title,
                Date = updateBookReadingEvent.Date,
                Location = updateBookReadingEvent.Location,
                StartTime = new TimeSpan(updateBookReadingEvent.Hours, updateBookReadingEvent.Minutes, 0),
                Duration = updateBookReadingEvent.Duration,
                Description = updateBookReadingEvent.Description,
                OtherDetails = updateBookReadingEvent.OtherDetails,
                Type = (Services.Models.EventType)updateBookReadingEvent.Type,
                InvitedUsers = updateBookReadingEvent.InvitedUsers
            };
            
            
            try
            {
                //to update the edited information
                if (eventService.UpdateDetails(updatedBookReadingEvent))
                    return RedirectToAction("Details", new { @id = id });
                else
                    return new HttpStatusCodeResult(HttpStatusCode.Conflict);

            }
            catch
            {
                return View("Edit", new { @id = id });
            }
        }

    }
}
