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
    /// <typeparam name="TMember"></typeparam>
    /// <param name="expression"></param>
    /// <returns></returns>
    IOGraphGdmEntityTypeDescriptor<T> HasKey<TMember>(Expression<Func<T, TMember>> expression) where TMember : struct;
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TMember"></typeparam>
    /// <param name="expression"></param>
    /// <returns></returns>
    IOGraphGdmEntityTypeDescriptor<T> HasKey<TMember>(Expression<Func<T, TMember?>> expression) where TMember : struct;
    /// <summary>
    /// Ignores the property on <typeparamref name="T"/>.
    /// </summary>
    /// <param name="label">The name of the property on <typeparamref name="T"/>.</param>
    /// <returns></returns>
    IOGraphGdmEntityTypeDescriptor<T> Ignore(Label label);
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TMember"></typeparam>
    /// <param name="expression"></param>
    /// <returns></returns>
    IOGraphGdmEntityTypeDescriptor<T> Ignore<TMember>(Expression<Func<T, TMember>> expression);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="label">The name of the property on <typeparamref name="T"/>.</param>
    /// <returns></returns>
    IOGraphGdmPropertyDescriptor HasProperty(Label label);
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TMember"></typeparam>
    /// <param name="expression"></param>
    /// <returns></returns>
    IOGraphGdmPropertyDescriptor<TMember?> HasProperty<TMember>(Expression<Func<T, TMember?>> expression);
}