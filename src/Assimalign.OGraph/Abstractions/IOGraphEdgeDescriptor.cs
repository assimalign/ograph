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
    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    IOGraphEdgeDescriptor UseMetadata(string key, object value);
    IOGraphEdgeDescriptor UseMiddleware<TMiddleware>() where TMiddleware : IOGraphEdgeMiddleware, new();
    IOGraphEdgeDescriptor UseMiddleware(IOGraphEdgeMiddleware middleware);
    IOGraphEdgeDescriptor UseMiddleware(OGraphEdgeMiddleware middleware);
    IOGraphEdgeDescriptor UseResolver<TResovler>() where TResovler : IOGraphEdgeResolver, new();
    IOGraphEdgeDescriptor UseResolver(IOGraphEdgeResolver resolver);
    IOGraphEdgeDescriptor UseResolver(OGraphEdgeResolver resolver);
}


