using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Assimalign.OGraph;

public interface IOGraphNodeDescriptor<T>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="label"></param>
    /// <returns></returns>
    IOGraphNodeDescriptor<T> HasLabel(Label label);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    IOGraphNodeDescriptor<T> HasMetadata(string key, object value);
    /// <summary>
    /// Specify a property to be ignored.
    /// </summary>
    /// <typeparam name="TProperty"></typeparam>
    /// <param name="expression"></param>
    /// <returns></returns>
    IOGraphNodeDescriptor<T> Ignore<TProperty>(Expression<Func<T, TProperty>> expression);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    IOGraphPropertyDescriptor HasProperty(Name name);
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TProperty"></typeparam>
    /// <param name="expression"></param>
    /// <returns></returns>
    IOGraphPropertyDescriptor<TProperty> HasProperty<TProperty>(Expression<Func<T, TProperty>> expression);
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TProperty"></typeparam>
    /// <param name="expression"></param>
    /// <returns></returns>
    IOGraphEdgeDescriptor<TProperty> HasEdge<TProperty>(Expression<Func<T, TProperty>> expression);
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TProperty"></typeparam>
    /// <param name="expression"></param>
    /// <returns></returns>
    IOGraphEdgeDescriptor<TProperty> HasEdge<TProperty>(Expression<Func<T, IEnumerable<TProperty>>> expression);
}