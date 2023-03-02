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
    IOGraphOperationDescriptor UseQuery(QueryParam query);
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
    /// <param name="middleware"></param>
    /// <returns></returns>
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

