using System;
using System.Linq.Expressions;

namespace Assimalign.OGraph;

public interface IOGraphComplexTypeDescriptor<T>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    IOGraphComplexTypeDescriptor<T> Ignore(Label name);
    /// <summary>
    /// Specify a property to be ignored.
    /// </summary>
    /// <typeparam name="TProperty"></typeparam>
    /// <param name="expression"></param>
    /// <returns></returns>
    IOGraphComplexTypeDescriptor<T> Ignore<TProperty>(Expression<Func<T, TProperty>> expression);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    IOGraphPropertyDescriptor HasProperty(Label name);
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TProperty"></typeparam>
    /// <param name="expression"></param>
    /// <returns></returns>
    IOGraphPropertyDescriptor<TProperty> HasProperty<TProperty>(Expression<Func<T, TProperty>> expression);
}
