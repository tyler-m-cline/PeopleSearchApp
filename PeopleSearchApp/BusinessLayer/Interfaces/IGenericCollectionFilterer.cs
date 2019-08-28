using System.Collections.Generic;

namespace PeopleSearchApp.BusinessLayer.Interfaces
{
    internal interface IGenericCollectionFilterer<T>
    {
        IEnumerable<T> ApplyCollectionFilter(IEnumerable<T> collectionToFilter, string filterString);
    }
}
