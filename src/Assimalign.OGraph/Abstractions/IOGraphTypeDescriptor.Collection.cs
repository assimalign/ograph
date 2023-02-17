using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

public interface IOGraphCollectionTypeDescriptor
{
}


public interface IOGraphCollectionTypeDescriptor<T> : IOGraphTypeDescriptor<T>
{
    IOGraphCollectionTypeDescriptor<T> UseFiltering();  // Enables filtering
    IOGraphCollectionTypeDescriptor<T> UseFiltering(Action<OGraphFilteringOptions> configure);
    IOGraphCollectionTypeDescriptor<T> UseSorting();    // Enables sorting
    IOGraphCollectionTypeDescriptor<T> UseSorting(Action<OGraphSortingOptions> configure);
    IOGraphCollectionTypeDescriptor<T> UsePaging();     // Enables paging, UseQueryableType, or UsePagingType
    IOGraphCollectionTypeDescriptor<T> UsePaging(Action<OGraphPagingOptions> configure);
    IOGraphCollectionTypeDescriptor<T> UseCollectionType(Action<IOGraphComplexTypeDescriptor<T>> configure);

}