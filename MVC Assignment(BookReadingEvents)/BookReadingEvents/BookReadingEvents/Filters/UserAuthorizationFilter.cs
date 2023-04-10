using BookReadingEvents.ViewModels;
using Services;
using Services.Infrastructure;
using System;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;

namespace BookReadingEvents.Filters
{
    public class UserAuthorizationFilter : ActionFilterAttribute, IAuthorizationFilter
    {
       
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            //checking if user exists and session["UserID"] is not empty
            if (string.IsNullOrEmpty(Convert.ToString(filterContext.HttpContext.Session["UserID"])))
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }
           
        }
        
    }
}