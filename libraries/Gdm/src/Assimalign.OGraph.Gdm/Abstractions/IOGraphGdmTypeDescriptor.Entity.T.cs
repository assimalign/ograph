using System;
using System.Linq.Expressions;

namespace Assimalign.OGraph.Gdm;

/// <summary>
/// 
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IOGraphGdmEntityTypeDescriptor<T> 
    where T : class, new()
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="label"></param>
    /// <returns></returns>
    IOGraphGdmEntityTypeDescriptor<T> HasLabel(Label label);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="label"></param>
    /// <returns></returns>
    IOGraphGdmEntityTypeDescriptor<T> HasKey(Label label);
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <param name="expression"></param>
    /// <returns></returns>
    IOGraphGdmEntityTypeDescriptor<T> HasKey<TKey>(Expression<Func<T, TKey>> expression) where TKey : struct;
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <param name="expression"></param>
    /// <returns></returns>
    IOGraphGdmEntityTypeDescriptor<T> HasKey<TKey>(Expression<Func<T, TKey?>> expression) where TKey : struct;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="label">The name of the property on <typeparamref name="T"/>.</param>
    /// <returns></returns>
    IOGraphGdmPropertyDescriptor HasProperty(Label label);
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TProperty"></typeparam>
    /// <param name="expression"></param>
    /// <returns></returns>
    IOGraphGdmPropertyDescriptor<TProperty?> HasProperty<TProperty>(Expression<Func<T, TProperty?>> expression);
}