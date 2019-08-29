using Microsoft.VisualStudio.TestTools.UnitTesting;
using PeopleSearchApp.BusinessLayer;
using PeopleSearchApp.DataAccessLayer.Models;
using System.Collections.Generic;
using System.Linq;

namespace PeopleSearchAppTest.BusinessLayerTests
{
    [TestClass]
    public class PersonNameSearchFiltererTests
    {
        private PersonNameSearchFilterer filterer;
        private List<Person> people;

        public PersonNameSearchFiltererTests()
        {
            filterer = new PersonNameSearchFilterer();
            people = new List<Person> {
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
            var personList = (result as IEnumerable<Person>).ToList();
            var person = personList[0];

            Assert.AreEqual(person.FirstName, "Han");
        }

        [TestMethod]
        public void ApplyCollectionFilterBlankFilterTest()
        {
            var result = filterer.ApplyCollectionFilter(people, "");
            var personList = (result as IEnumerable<Person>).ToList();
            var areEqual = personList.SequenceEqual(people);

            Assert.IsTrue(areEqual);
        }

        [TestMethod]
        public void ApplyCollectionFilterNullFilterTest()
        {
            var result = filterer.ApplyCollectionFilter(people, null);
            var personList = (result as IEnumerable<Person>).ToList();
            var areEqual = personList.SequenceEqual(people);

            Assert.IsTrue(areEqual);
        }
    }
}
