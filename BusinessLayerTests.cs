using Microsoft.VisualStudio.TestTools.UnitTesting;
using PeopleSearchApp.BusinessLayer;
using PeopleSearchApp.DataAccessLayer.Models;
using System.Collections.Generic;

namespace PeopleSearchAppTest.BusinessLayerTests
{
    [TestClass]
    class PersonNameSearchFiltererTests
    {
        private PersonNameSearchFilterer filterer;
        private Person[] people;

        public PersonNameSearchFiltererTests()
        {
            filterer = new PersonNameSearchFilterer();
            people = new Person[] {
                new Person()
                {
                    FirstName = "Han",
                    LastName = "Solo"
                },
                new Person()
                {
                    FirstName = "Luke",
                    LastName = "Skywalker"
                },
                new Person()
                {
                    FirstName = "Chew",
                    LastName = "Bacca"
                }
            };
        }

        [TestMethod]
        public void ApplyCollectionFilterTest()
        {
            var result = filterer.ApplyCollectionFilter(people, "Han s");
            Assert.AreEqual((result as List<Person>)[0].FirstName, "Han"); 
        }

        [TestMethod]
        public void ApplyCollectionFilterBlankFilterTest()
        {
            var result = filterer.ApplyCollectionFilter(people, "");
            Assert.AreEqual((result as List<Person>), people);
        }

        [TestMethod]
        public void ApplyCollectionFilterNullFilterTest()
        {
            var result = filterer.ApplyCollectionFilter(people, null);
            Assert.AreEqual((result as List<Person>), people);
        }
    }
}
