using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

public interface IOGraphVertexEdgeDescriptor
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TVertex"></typeparam>
    /// <returns></returns>
    IOGraphEdgeDescriptor UseTarget<TVertex>()
        where TVertex : IOGraphVertex, new();
    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    IOGraphEdgeDescriptor UseMetadata(string key, object value);
}
