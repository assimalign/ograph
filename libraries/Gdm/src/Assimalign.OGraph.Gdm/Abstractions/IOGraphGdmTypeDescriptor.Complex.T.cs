using System;
using System.Linq.Expressions;

namespace Assimalign.OGraph.Gdm;

public interface IOGraphGdmComplexTypeDescriptor<T> 
    where T : class, new()
{
    /// <summary>
    /// Overrides the default type name.
    /// </summary>
    /// <param name="label"></param>
    /// <returns></returns>
    IOGraphGdmComplexTypeDescriptor<T> HasLabel(Label label);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    IOGraphGdmPropertyDescriptor HasProperty(Label name);

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TProperty"></typeparam>
    /// <param name="expression"></param>
    /// <returns></returns>
    IOGraphGdmPropertyDescriptor<TProperty> HasProperty<TProperty>(Expression<Func<T, TProperty>> expression);

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TFunction"></typeparam>
    /// <param name="expression"></param>
    /// <returns></returns>
    IOGraphGdmFunctionDescriptor<TFunction> HasFunction<TFunction>(Expression<Func<T, TFunction>> expression);
}
