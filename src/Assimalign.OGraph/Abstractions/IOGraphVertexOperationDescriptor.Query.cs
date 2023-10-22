using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

public interface IOGraphVertexQueryOperationDescriptor
{
    IOGraphVertexQueryOperationDescriptor UseRoute(Route route);
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TMiddleware"></typeparam>
    /// <returns>The current descriptor.</returns>
    IOGraphVertexQueryOperationDescriptor UseMiddleware<TMiddleware>() where TMiddleware : IOGraphOperationMiddleware, new();
    /// <summary>
    /// 
    /// </summary>
    /// <param name="middleware"></param>
    /// <returns>The current descriptor.</returns>
    IOGraphVertexQueryOperationDescriptor UseMiddleware(IOGraphOperationMiddleware middleware);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="middleware"></param>
    /// <returns>The current descriptor.</returns>
    IOGraphVertexQueryOperationDescriptor UseMiddleware(OGraphOperationMiddleware middleware);
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TResolver"></typeparam>
    /// <returns>The current descriptor.</returns>
    IOGraphVertexQueryOperationDescriptor UseResolver<TResolver>() where TResolver : IOGraphOperationResolver, new();
    /// <summary>
    /// 
    /// </summary>
    /// <param name="resolver"></param>
    /// <returns>The current descriptor.</returns>
    IOGraphVertexQueryOperationDescriptor UseResolver(IOGraphOperationResolver resolver);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="resolver"></param>
    /// <returns>The current descriptor.</returns>
    IOGraphVertexQueryOperationDescriptor UseResolver(OGraphOperationResolver resolver);
    /// <summary>
    /// Overrides the default query provider.
    /// </summary>
    /// <typeparam name="TQueryProvider"></typeparam>
    /// <returns>The current descriptor.</returns>
    IOGraphVertexQueryOperationDescriptor UseQueryProvider<TQueryProvider>() where TQueryProvider : IOGraphQueryProvider, new();
    /// <summary>
    /// Overrides the default query provider.
    /// </summary>
    /// <param name="queryProvider"></param>
    /// <returns>The current descriptor.</returns>
    IOGraphVertexQueryOperationDescriptor UseQueryProvider(IOGraphQueryProvider queryProvider);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="options"></param>
    /// <returns>The current descriptor.</returns>
    IOGraphVertexQueryOperationDescriptor UseQueryOptions(OGraphQueryOptions options);
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TQueryOptions"></typeparam>
    /// <param name="configure"></param>
    /// <returns>The current descriptor.</returns>
    IOGraphVertexQueryOperationDescriptor UseQueryOptions<TQueryOptions>(Action<TQueryOptions> configure) where TQueryOptions : OGraphQueryOptions, new();
    /// <summary>
    /// 
    /// </summary>
    /// <param name="configure"></param>
    /// <returns>The current descriptor.</returns>
    IOGraphVertexQueryOperationDescriptor UseQueryOptions(Action<OGraphQueryOptions> configure);

}
