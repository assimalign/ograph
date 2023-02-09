using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

public interface IOGraphQueryDescriptor
{
    IOGraphQueryDescriptor UseRoute(Route route);
    IOGraphQueryDescriptor UseMethod(Method method);

    IOGraphQueryDescriptor UseFiltering();  // Enables filtering
    IOGraphQueryDescriptor UseSorting();    // Enables sorting
    IOGraphQueryDescriptor UsePaging();     // Enables paging, UseQueryableType, or UsePagingType

    IOGraphQueryDescriptor UseType<T>();
    IOGraphQueryDescriptor UseType<T>(Action<IOGraphQueryTypeDescriptor<T>> descriptor);
    IOGraphQueryDescriptor UseType(Type type, Action<IOGraphQueryTypeDescriptor> descriptor);

    IOGraphQueryDescriptor UseEnumerableType<T>();
    IOGraphQueryDescriptor UseEnumerableType<T>(Action<IOGraphQueryTypeDescriptor<T>> descriptor);
    IOGraphQueryDescriptor UseEnumerableType(Type type, Action<IOGraphQueryTypeDescriptor> descriptor);

    IOGraphQueryDescriptor UseQueryableType<T>();
    IOGraphQueryDescriptor UseQueryableType<T>(Action<IOGraphQueryTypeDescriptor<T>> descriptor);
    IOGraphQueryDescriptor UseQueryableType(Type type, Action<IOGraphQueryTypeDescriptor> descriptor);

    IOGraphQueryDescriptor UseAuthorization();


    void UseResolver(OGraphResolver<object> resolver);
    void UseResolver<T>(OGraphResolver<T> ressolver);
}
