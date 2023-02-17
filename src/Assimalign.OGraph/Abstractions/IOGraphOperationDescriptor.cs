using System;

namespace Assimalign.OGraph;


public interface IOGraphOperationDescriptor
{
    IOGraphOperationDescriptor UseRoute(Route route);
    IOGraphOperationDescriptor UseMethod(Method method);
    IOGraphOperationDescriptor UseQuery(QueryParam query);

    /// <summary>
    /// Binds the operation to a specific node.
    /// </summary>
    /// <remarks></remarks>
    /// <param name="label"></param>
    /// <returns></returns>
    IOGraphOperationDescriptor UseNodes(Label label);

    IOGraphOperationDescriptor UseMiddleware(IOGraphOperationMiddleware middleware);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="resolver"></param>
    /// <returns></returns>
    IOGraphOperationDescriptor UseResolver(IOGraphOperationResolver resolver);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="resolver"></param>
    /// <returns></returns>
    IOGraphOperationDescriptor UseResolver(OGraphOperationResolver resolver);
}

public interface IOGraphOperationDescriptor<T>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="route"></param>
    /// <returns></returns>
    IOGraphOperationDescriptor<T> UseRoute(Route route);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="method"></param>
    /// <returns></returns>
    IOGraphOperationDescriptor<T> UseMethod(Method method);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    IOGraphOperationDescriptor<T> UseQuery(QueryParam query);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="middleware"></param>
    /// <returns></returns>
    IOGraphOperationDescriptor<T> UseMiddleware(IOGraphOperationMiddleware middleware);




    ///// <summary>
    ///// Builds a derived type from <typeparamref name="T"/>
    ///// </summary>
    ///// <param name="name"></param>
    ///// <param name="descriptor"></param>
    ///// <returns></returns>
    //IOGraphOperationDescriptor<T> UseComplexRequestType(Label name, Action<IOGraphComplexTypeDescriptor<T>> descriptor);
    
    //IOGraphOperationDescriptor<T> UseCollectionRequestType(Action<IOGraphCollectionTypeDescriptor<T>> descriptor);
    ///// <summary>
    ///// 
    ///// </summary>
    ///// <param name="name"></param>
    ///// <param name="descriptor"></param>
    ///// <returns></returns>
    //IOGraphOperationDescriptor<T> UseCollectionRequestType(Label name, Action<IOGraphCollectionTypeDescriptor<T>> descriptor);

    //IOGraphOperationDescriptor<T> UseComplexResponseType(Action<IOGraphComplexTypeDescriptor<T>> descriptor);
    //IOGraphOperationDescriptor<T> UseComplexResponseType(Label name, Action<IOGraphComplexTypeDescriptor<T>> descriptor);
    //IOGraphOperationDescriptor<T> UseCollectionResponseType(Action<IOGraphCollectionTypeDescriptor<T>> descriptor);
    
    ///// <summary>
    ///// 
    ///// </summary>
    ///// <param name="name"></param>
    ///// <param name="descriptor"></param>
    ///// <returns></returns>
    //IOGraphOperationDescriptor<T> UseCollectionResponseType(Label name, Action<IOGraphCollectionTypeDescriptor<T>> descriptor);


    /// <summary>
    /// 
    /// </summary>
    /// <param name="resolver"></param>
    /// <returns></returns>
    IOGraphOperationDescriptor<T> UseResolver(IOGraphOperationResolver resolver);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="resolver"></param>
    /// <returns></returns>
    IOGraphOperationDescriptor<T> UseResolver(OGraphOperationResolver resolver);
}