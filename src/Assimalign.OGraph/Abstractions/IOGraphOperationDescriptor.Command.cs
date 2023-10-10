namespace Assimalign.OGraph;

/// <summary>
/// 
/// </summary>
public interface IOGraphCommandOperationDescriptor
{
    /// <summary>
    /// Sets the name of the operation
    /// </summary>
    /// <param name="name">A string name.</param>
    /// <returns>The current descriptor.</returns>
    IOGraphCommandOperationDescriptor UseName(Name name);
    /// <summary>
    /// Sets the route to use for the operation.
    /// </summary>
    /// <param name="route">The route value.</param>
    /// <returns>The current descriptor.</returns>
    IOGraphCommandOperationDescriptor UseRoute(Route route);
    /// <summary>
    /// Sets the HTTP method for the operaiton.
    /// </summary>
    /// <param name="method">The HTTP Method</param>
    /// <returns>The current descriptor.</returns>
    IOGraphCommandOperationDescriptor UseMethod(Method method);
    /// <summary>
    /// Sets and exposes a generic query parameter.
    /// </summary>
    /// <param name="query"></param>
    /// <returns>The current descriptor.</returns>
    IOGraphCommandOperationDescriptor UseQueryParam(string paramKey);
    /// <summary>
    /// Binds a node to the operation.
    /// </summary>
    /// <remarks></remarks>
    /// <param name="label"></param>
    /// <returns>The current descriptor.</returns>
    IOGraphCommandOperationDescriptor UseNode(Name name);
    /// <summary>
    /// Binds a node to the operation. 
    /// </summary>
    /// <typeparam name="TNode"></typeparam>
    /// <returns>The current descriptor.</returns>
    IOGraphCommandOperationDescriptor UseNode<TNode>() where TNode : IOGraphNode, new();
    
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TMiddleware"></typeparam>
    /// <returns>The current descriptor.</returns>
    IOGraphCommandOperationDescriptor UseMiddleware<TMiddleware>() where TMiddleware : IOGraphOperationMiddleware, new();
    /// <summary>
    /// 
    /// </summary>
    /// <param name="middleware"></param>
    /// <returns>The current descriptor.</returns>
    IOGraphCommandOperationDescriptor UseMiddleware(IOGraphOperationMiddleware middleware);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="middleware"></param>
    /// <returns>The current descriptor.</returns>
    IOGraphCommandOperationDescriptor UseMiddleware(OGraphOperationMiddleware middleware);
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TResolver"></typeparam>
    /// <returns>The current descriptor.</returns>
    IOGraphCommandOperationDescriptor UseResolver<TResolver>() where TResolver : IOGraphOperationResolver, new();
    /// <summary>
    /// 
    /// </summary>
    /// <param name="resolver"></param>
    /// <returns>The current descriptor.</returns>
    IOGraphCommandOperationDescriptor UseResolver(IOGraphOperationResolver resolver);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="resolver"></param>
    /// <returns>The current descriptor.</returns>
    IOGraphCommandOperationDescriptor UseResolver(OGraphOperationResolver resolver);
}