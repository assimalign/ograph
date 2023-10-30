using System;
using System.Linq.Expressions;

namespace Assimalign.OGraph;

public interface IOGraphVertexDescriptor<T>
    where T : class, new()
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TMember"></typeparam>
    /// <param name="member"></param>
    /// <returns></returns>
    IOGraphVertexDescriptor<T> HasKey<TMember>(Expression<Func<T, TMember>> member);
    /// <summary>
    /// Sets the unique label for the defined vertex.
    /// </summary>
    /// <param name="label"></param>
    /// <returns></returns>
    IOGraphVertexDescriptor<T> HasLabel(Label label);
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="configure"></param>
    /// <returns></returns>
    IOGraphVertexDescriptor<T> HasType(Action<IOGraphComplexTypeDescriptor<T>> configure);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="label"></param>
    /// <returns></returns>
    IOGraphVertexEdgeDescriptor HasEdge(Label label);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="label"></param>
    /// <returns></returns>
    IOGraphVertexQueryOperationDescriptor HasQuery(Label label);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <returns>The current descriptor.</returns>
    IOGraphVertexDescriptor<T> HasMetadata(string key, object value);
}