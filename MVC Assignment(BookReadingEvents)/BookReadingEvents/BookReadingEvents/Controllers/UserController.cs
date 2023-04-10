using BookReadingEvents.ViewModels;
using Services;
using Services.Models;
using Sevices.Infrastructure;
using System.Web.Mvc;

namespace BookReadingEvents.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return RedirectToAction("Login");
        }

        // GET: User/Login
        public ActionResult Login()
        {
            Session["loggedIn"] = false;
            Session["UserID"] = "";
            return View();
        }

        // POST: User/Login
        [HttpPost]
        public ActionResult Login(UserLogin userLogin)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            User newUser = new User()
            {
                EmailID = userLogin.EmailID,
                Password = userLogin.Password
            };

            IUserService userService = new ServiceFactory().GetUserService();

            //Authenticate the user
            if (userService.IsValid(newUser))
            {
                Session["loggedIn"] = true;
                Session["UserID"] = newUser.EmailID;
                return RedirectToAction("Index", "BookReadingEvents");
            }

            ModelState.AddModelError("", "  Invalid Credentials.");
            return View();

        }

        // GET: User/LogOut
        public ActionResult Logout()
        {
            //setting session variables to default value before logging out
            Session["loggedIn"] = false;
            Session["UserID"] = "";
            return RedirectToAction("Index", "BookReadingEvents");
        }


        // GET: User/Register
        public ActionResult Register()
        {
            Session["loggedIn"] = false;
            return View();
        }

        // POST: User/Register
        [HttpPost]
        public ActionResult Register(User user)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            IUserService userService = new ServiceFactory().GetUserService();

            User newUser = new User()
            {
                EmailID = user.EmailID,
                Password = user.Password,
                FullName = user.FullName
            };

            //Creating a new user and redirecting the user to login page
            if (userService.Create(newUser))
                return RedirectToAction("Login");
            else
            {
                ModelState.AddModelError("","Email ID Already registered.");
                return View();
            }
        }

        // Get: /customer-support
        //CUSTOMER SUPPORT ROUTE redirects to nagarro helpdesk
        public ActionResult CustomerSupport()
        {
            return Redirect("Https://helpdesk.nagarro.com");
        }
    }
}
