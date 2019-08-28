using System.Collections.Generic;
using System.Linq;
using PeopleSearchApp.DataAccessLayer.Models;
using PeopleSearchApp.BusinessLayer.Interfaces;

namespace PeopleSearchApp.BusinessLayer
{
    public class PersonNameSearchFilterer : IGenericCollectionFilterer<Person>
    {
        public IEnumerable<Person> ApplyCollectionFilter(IEnumerable<Person> collectionToFilter, string filterString)
        {
            var filteredList = collectionToFilter;

            if (!string.IsNullOrWhiteSpace(filterString))
            {
                filterString = filterString.ToUpper();

                filteredList = collectionToFilter.Where(person => {
                    var personName = string.Format("{0} {1}", person.FirstName, person.LastName)
                        .ToUpper();

                    return personName.Contains(filterString);
                });
            }

            return filteredList;
        }
    }
}
