using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

public interface IOGraphEdgeDescriptor
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="label">The name of the node within the OGraph Model.</param>
    /// <returns></returns>
    IOGraphEdgeDescriptor UseTargetNode(Label label);
    IOGraphEdgeDescriptor UseTargetNode<TNode>() where TNode : IOGraphNode, new();
    IOGraphEdgeDescriptor UseSourceNode(Label label);
    IOGraphEdgeDescriptor UseSourceNode<TNode>() where TNode : IOGraphNode, new();
    IOGraphEdgeDescriptor UseMetadata(string key, object value);
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TQueryProvider"></typeparam>
    /// <returns></returns>
    IOGraphEdgeDescriptor UseQueryProvider<TQueryProvider>() where TQueryProvider : IOGraphQueryProvider, new();
    /// <summary>
    /// 
    /// </summary>
    /// <param name="queryProvider"></param>
    /// <returns></returns>
    IOGraphEdgeDescriptor UseQueryProvider(IOGraphQueryProvider queryProvider);
    IOGraphEdgeDescriptor UseMiddleware<TMiddleware>() where TMiddleware : IOGraphEdgeMiddleware, new();
    IOGraphEdgeDescriptor UseMiddleware(IOGraphEdgeMiddleware middleware);
    IOGraphEdgeDescriptor UseMiddleware(OGraphEdgeMiddleware middleware);
    IOGraphEdgeDescriptor UseResolver<TResovler>() where TResovler : IOGraphEdgeResolver, new();
    IOGraphEdgeDescriptor UseResolver(IOGraphEdgeResolver resolver);
    IOGraphEdgeDescriptor UseResolver(OGraphEdgeResolver resolver);
}


