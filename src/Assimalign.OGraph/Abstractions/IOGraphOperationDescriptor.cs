using System;

namespace Assimalign.OGraph;


public interface IOGraphOperationDescriptor
{



    /// <summary>
    /// 
    /// </summary>
    /// <param name="route"></param>
    /// <returns></returns>
    IOGraphOperationDescriptor UseRoute(Route route);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="method"></param>
    /// <returns></returns>
    IOGraphOperationDescriptor UseMethod(Method method);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    IOGraphOperationDescriptor UseQuery(QueryValue query);
    /// <summary>
    /// Binds the operation to a specific node.
    /// </summary>
    /// <remarks></remarks>
    /// <param name="label"></param>
    /// <returns></returns>
    IOGraphOperationDescriptor UseNodes(Label label);
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TType"></typeparam>
    /// <returns></returns>
    IOGraphOperationDescriptor UseRequestType<TType>() where TType : IOGraphType, new();
    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    IOGraphOperationDescriptor UseRequestType(IOGraphType type);
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TType"></typeparam>
    /// <returns></returns>
    IOGraphOperationDescriptor UseResponseType<TType>() where TType : IOGraphType, new();
    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    /// <remarks>
    /// The Execution engine internally will try
    /// </remarks>
    IOGraphOperationDescriptor UseResponseType(IOGraphType type);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="middleware"></param>
    /// <returns></returns>
    IOGraphOperationDescriptor UseMiddleware(IOGraphOperationMiddleware middleware);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="middleware"></param>
    /// <returns></returns>
    IOGraphOperationDescriptor UseMiddleware(OGraphOperationMiddleware middleware);
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
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="resolver"></param>
    /// <returns></returns>
    //IOGraphOperationDescriptor UseResolver<T>(OGraphOperationResolver<T> resolver);

    ///// <summary>
    ///// Builds a derived type from <typeparamref name="TProperty"/>
    ///// </summary>
    ///// <param name="name"></param>
    ///// <param name="descriptor"></param>
    ///// <returns></returns>
    //IOGraphOperationDescriptor<TProperty> UseComplexRequestType(Label name, Action<IOGraphComplexTypeDescriptor<TProperty>> descriptor);

    //IOGraphOperationDescriptor<TProperty> UseCollectionRequestType(Action<IOGraphCollectionTypeDescriptor<TProperty>> descriptor);
    ///// <summary>
    ///// 
    ///// </summary>
    ///// <param name="name"></param>
    ///// <param name="descriptor"></param>
    ///// <returns></returns>
    //IOGraphOperationDescriptor<TProperty> UseCollectionRequestType(Label name, Action<IOGraphCollectionTypeDescriptor<TProperty>> descriptor);

    //IOGraphOperationDescriptor<TProperty> UseComplexResponseType(Action<IOGraphComplexTypeDescriptor<TProperty>> descriptor);
    //IOGraphOperationDescriptor<TProperty> UseComplexResponseType(Label name, Action<IOGraphComplexTypeDescriptor<TProperty>> descriptor);
    //IOGraphOperationDescriptor<TProperty> UseCollectionResponseType(Action<IOGraphCollectionTypeDescriptor<TProperty>> descriptor);

    ///// <summary>
    ///// 
    ///// </summary>
    ///// <param name="name"></param>
    ///// <param name="descriptor"></param>
    ///// <returns></returns>
    //IOGraphOperationDescriptor<TProperty> UseCollectionResponseType(Label name, Action<IOGraphCollectionTypeDescriptor<TProperty>> descriptor);
}

