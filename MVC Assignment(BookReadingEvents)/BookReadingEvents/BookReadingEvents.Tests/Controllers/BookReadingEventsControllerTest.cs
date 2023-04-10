using BookReadingEvents.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace BookReadingEvents.Tests.Controllers
{
    //MOCKING THE SESSION VARIABLES
    public class MockHttpSession: HttpSessionStateBase
    {
        Dictionary<string, object> _sessionDictionary = new Dictionary<string, object>();
  
        public override object this[string UserID]
        {
            get { return _sessionDictionary[UserID]; }
            set { _sessionDictionary[UserID] = value; }
        }
    }

    [TestClass]
    public class BookReadingEventsControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            var context = new Mock<ControllerContext>();
            var session = new MockHttpSession();

            context.Setup(m => m.HttpContext.Session).Returns(session);
            BookReadingEventsController controller = new BookReadingEventsController();

            controller.ControllerContext = context.Object;

            session["UserID"] = "myadmin@bookevents.com";
            session["LoggedIn"] = true;

            // Act
            var result = controller.Index();

            // Assert
            //Assert.IsInstanceOfType(typeof(ViewResult),result.GetType());
            var resultView = result as ViewResult;
            Assert.AreEqual("", resultView.ViewName); // viewname not passed as argument
            
            
        }

        [TestMethod]
        public void Details()
        {
            // Arrange
            var context = new Mock<ControllerContext>();
            var session = new MockHttpSession();

            context.Setup(m => m.HttpContext.Session).Returns(session);
            BookReadingEventsController controller = new BookReadingEventsController();

            controller.ControllerContext = context.Object;
            // System.Diagnostics.Debug.WriteLine(result.GetType());
            session["UserID"] = "myadmin@bookevents.com";
            session["LoggedIn"] = true;

            // Act
            var result = controller.Details(1000); //Event with id=1000; does`nt exist.

            // Assert
            Assert.AreEqual(typeof(HttpNotFoundResult), result.GetType());

            
            //TRYING TO ACCESSS A PRIVATE EVENT BY ADMIN
            session["UserID"] = "myadmin@bookevents.com";
            
            // Act
            var result2 = controller.Details(2) as ViewResult; //Event with id=2; is private

            // Assert
            Assert.AreEqual("", result2.ViewName);



            //TRYING TO ACCESSS A PRIVATE EVENT by ANONYMOUS user
            session["UserID"] = "abc@xyz.com";

            // Act
            var result3 = controller.Details(2) as ViewResult; //Event with id=2; is private

            // Assert
            Assert.AreEqual("NotAuthorized", result3.ViewName); 

        }

    }
}
