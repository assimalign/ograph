using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

public interface IOGraphNodeDescriptor
{
    IOGraphNodeDescriptor AddName(Name name);
}

public interface IOGraphNodeDescriptor<T>
{
    /// <summary>
    /// Overrides the default Node Name.
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    IOGraphNodeDescriptor<T> AddName(Name name);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <param name="descriptor"></param>
    /// <returns></returns>
    IOGraphNodeDescriptor<T> AddOperation(Name name, Action<IOGraphOperationDescriptor> descriptor);
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEdge"></typeparam>
    /// <param name="name"></param>
    /// <param name="descriptor"></param>
    /// <returns></returns>
    IOGraphNodeDescriptor<T> AddEdge<TEdge>(Name name, Action<IOGraphEdgeDescriptor<TEdge>> descriptor);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="descriptor"></param>
    /// <returns></returns>
    IOGraphNodeDescriptor<T> AddType(Action<IOGraphTypeDescriptor<T>> descriptor);
        
}
