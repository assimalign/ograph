using System;
using System.Linq.Expressions;

namespace Assimalign.OGraph;

public interface IOGraphVertexDescriptor<T>
    where T : class, new()
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <returns>The current descriptor.</returns>
    IOGraphVertexDescriptor<T> HasMetadata(string key, object value);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="label"></param>
    /// <returns></returns>
    IOGraphVertexDescriptor<T> HasLabel(Name label);
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TMember"></typeparam>
    /// <param name="key"></param>
    /// <returns></returns>
    IOGraphVertexDescriptor<T> HasKey(Name key);
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TMember"></typeparam>
    /// <param name="expression"></param>
    /// <returns></returns>
    IOGraphVertexDescriptor<T> HasKey<TMember>(Expression<Func<T, TMember>> expression);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    IOGraphPropertyDescriptor HasProperty(Name name);
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TMember"></typeparam>
    /// <param name="expression"></param>
    /// <returns></returns>
    IOGraphPropertyDescriptor<TMember> HasProperty<TMember>(Expression<Func<T, TMember>> expression);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    IOGraphVertexDescriptor<T> Ignore(Name name);
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TMember"></typeparam>
    /// <param name="expression"></param>
    /// <returns></returns>
    IOGraphVertexDescriptor<T> Ignore<TMember>(Expression<Func<T, TMember>> expression);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="label"></param>
    /// <returns></returns>
    IOGraphVertexEdgeDescriptor HasEdge(Name label);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="label"></param>
    /// <returns></returns>
    IOGraphVertexQueryOperationDescriptor HasQuery(Name label);

}