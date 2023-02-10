using Assimalign.OGraph;
using System;

namespace Assimalign.OGraph;

public interface IOGraphOperationDescriptor
{
    IOGraphOperationDescriptor UseRoute(Route route);
    IOGraphOperationDescriptor UseMethod(Method method);

    IOGraphOperationDescriptor UseValidation();

    IOGraphOperationDescriptor UseQuery()

    IOGraphOperationDescriptor UseFiltering();  // Enables filtering
    IOGraphOperationDescriptor UseFiltering(Action<OGraphFilteringOptions> configure);
    IOGraphOperationDescriptor UseSorting();    // Enables sorting
    IOGraphOperationDescriptor UseSorting(Action<OGraphSortingOptions> configure);
    IOGraphOperationDescriptor UsePaging();     // Enables paging, UseQueryableType, or UsePagingType
    IOGraphOperationDescriptor UsePaging(Action<OGraphPagingOptions> configure);

    /// <summary>
    /// Specifies the type to expect in the Body of the request.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    IOGraphOperationDescriptor UseRequestType(Name typeName);
    IOGraphOperationDescriptor UseRequestType<T>();
    IOGraphOperationDescriptor UseRequestType<T>(Action<IOGraphComplexTypeDescriptor<T>> descriptor);
    IOGraphOperationDescriptor UseRequestType(Type type, Action<IOGraphComplexTypeDescriptor> descriptor);

    IOGraphOperationDescriptor UseResponseType<T>();
    IOGraphOperationDescriptor UseResponseType<T>(Action<IOGraphTypeDescriptor<T>> descriptor);
    IOGraphOperationDescriptor UseResponseType(Type type, Action<IOGraphTypeDescriptor> descriptor);
    IOGraphOperationDescriptor UseEnumerableResponseType<T>();
    IOGraphOperationDescriptor UseEnumerableResponseType<T>(Action<IOGraphTypeDescriptor<T>> descriptor);
    IOGraphOperationDescriptor UseEnumerableResponseType(Type type, Action<IOGraphTypeDescriptor> descriptor);
    IOGraphOperationDescriptor UseQueryableResponseType<T>();
    IOGraphOperationDescriptor UseQueryableResponseType<T>(Action<IOGraphTypeDescriptor<T>> descriptor);
    IOGraphOperationDescriptor UseQueryableResponseType(Type type, Action<IOGraphTypeDescriptor> descriptor);


    IOGraphOperationDescriptor UseResolver(IOGraphOperationResolver resolver);

    IOGraphOperationDescriptor UseResolver(OGraphOperationResolver resolver);


}
