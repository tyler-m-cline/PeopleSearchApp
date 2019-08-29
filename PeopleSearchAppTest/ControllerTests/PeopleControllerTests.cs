using CodingAssignment.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PeopleSearchApp.BusinessLayer;
using PeopleSearchApp.DataAccessLayer.DBContexts;
using PeopleSearchApp.DataAccessLayer.Initializers;
using PeopleSearchApp.DataAccessLayer.Models;
using System.Linq;

namespace PeopleSearchAppTest.ControllerTests
{
    [TestClass]
    public class PeopleControllerTests
    {
        private PeopleDBContext context;

        public PeopleControllerTests()
        {
            DbContextOptionsBuilder<PeopleDBContext> options = new DbContextOptionsBuilder<PeopleDBContext>();
            options.UseInMemoryDatabase("Server=(localdb)\\mssqllocaldb;Database=EFProviders.InMemory;Trusted_Connection=True;ConnectRetryCount=0");

            context = new PeopleDBContext(options.Options);
            if (context.People.ToList().Count == 0)
            {
                PeopleDBInitializer peopleDBInitializer = new PeopleDBInitializer(context);
                peopleDBInitializer.SeedDBContext();
            }
        }

        [TestMethod]
        public void GetPeoplePeopleWithSearchStringTest()
        {
            var controller = new PeopleController(context);
            var filterer = new PersonNameSearchFilterer();
            var filter = "Han";

            var result = controller.GetPeople(filter);
            var expectedResult = filterer.ApplyCollectionFilter(context.People.ToListAsync().Result, filter);
            var arePeopleListsEqual = result.SequenceEqual(expectedResult);

            Assert.IsTrue(arePeopleListsEqual);
        }

        [TestMethod]
        public void GetPeoplePeopleWithoutSearchStringTest()
        {
            var controller = new PeopleController(context);

            var result = controller.GetPeople("");
            var expectedResult = context.People.ToList();

            var arePeopleListsEqual = result.SequenceEqual(expectedResult);

            Assert.IsTrue(arePeopleListsEqual);
        }

        [TestMethod]
        public void GetPersonTest()
        {
            var controller = new PeopleController(context);

            var id = (int)PeopleDBInitializer.PeopleIds.hanId;
            var result = controller.GetPerson(id).Result;
            var expectedResult = context.People.Find(id);

            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            Assert.AreEqual((result as OkObjectResult).Value, expectedResult);
        }

        [TestMethod]
        public void GetPersonNotFoundTest()
        {
            var controller = new PeopleController(context);
            var result = controller.GetPerson(0).Result;

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void PutPersonTest()
        {
            var controller = new PeopleController(context);
            var id = (int)PeopleDBInitializer.PeopleIds.hanId;
            var person = new Person()
            {
                Id = id,
                FirstName = "Dan",
                LastName = "Smith",
                Age = 25,
                StreetAddress = "111 Space Smuggler Road",
                City = "Chicago",
                State = "IL",
                ZipCode = 555556,
                Photograph = ""
            };

            var result = controller.PutPerson(id, person).Result;

            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            Assert.AreEqual((result as OkObjectResult).Value, person);
        }

        [TestMethod]
        public void PutPersonBadRequestTest()
        {
            var controller = new PeopleController(context);

            var result = controller.PutPerson(0, new Person() { Id = 1}).Result;

            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        [TestMethod]
        public void PostPersonTest()
        {
            var controller = new PeopleController(context);
            var person = new Person()
            {
                FirstName = "Dan",
                LastName = "Smith",
                Age = 25,
                StreetAddress = "111 Space Smuggler Road",
                City = "Millennium Falcon",
                State = "Space",
                ZipCode = 555556,
                Photograph = ""
            };

            var result = controller.PostPerson(person).Result;

            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public void DeletePersonTest()
        {
            var controller = new PeopleController(context);
            var id = (int)PeopleDBInitializer.PeopleIds.chewyId;
            var result = controller.DeletePerson(id).Result;

            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public void DeletePersonNotFoundTest()
        {
            var controller = new PeopleController(context);
            var result = controller.DeletePerson(0).Result;

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
    }
}
