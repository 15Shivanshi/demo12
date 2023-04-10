using System;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using System.Web.Routing;

namespace BookReadingEvents.Filters
{
    public class UserAuthenticationFilter : ActionFilterAttribute, IAuthenticationFilter
    {
        //checking if user is logged in
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            if ((string.IsNullOrEmpty(Convert.ToString(filterContext.HttpContext.Session["LoggedIn"])) ||
                    Convert.ToBoolean(filterContext.HttpContext.Session["LoggedIn"])==false))
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }
        }

        //Return to login page if user is not authenticated
        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            if (filterContext.Result == null || filterContext.Result is HttpUnauthorizedResult)
            {
                filterContext.Result = new RedirectToRouteResult(
                new RouteValueDictionary
                {
                    { "controller", "User" },
                    { "action", "Login" }
                });
            }
        }
    
        
    }
}