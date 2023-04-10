using BookReadingEvents.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Services.Models;
using System.Web.Mvc;

namespace BookReadingEvents.Tests.Controllers
{
    [TestClass]
    public class UserControllerTest
    {
        [TestMethod]
        [HttpPost]
        public void Register()
        {
            // Arrange
            UserController controller = new UserController();

            // Act

            ViewResult result = controller.Register(new User
            {
                EmailID = "myadmin@bookevents.com",
                Password = "12345@",
                FullName = "admin"
            }) as ViewResult;

            ViewResult result1 = controller.Register(new User
            {
                EmailID = "newUser@gmail.com",
                Password = "myNewPassword",
                FullName = "new user"
            }) as ViewResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("", result.ViewName); //empty because admin is already registered
            Assert.IsTrue(controller.ViewData.ModelState.Count == 1, "Email ID Already registered.");

            Assert.IsNotNull(result1);
            Assert.AreEqual("", result1.ViewName); //empty because admin is already registered
            Assert.IsTrue(controller.ViewData.ModelState.Count == 1, " 1. Password Lenght must be more than 5. <br>" +
            "2. Include atleast one special character (!@#%&*).");
        }

        [TestMethod]
        public void CustomerSupport()
        {
            // Arrange
            UserController controller = new UserController();

            // Act
            ActionResult result = controller.CustomerSupport();

            //Assert
            Assert.IsInstanceOfType(result, typeof(RedirectResult));

            RedirectResult rr = result as RedirectResult;
            Assert.AreEqual("Https://helpdesk.nagarro.com", rr.Url);
            

        }
    }
}
