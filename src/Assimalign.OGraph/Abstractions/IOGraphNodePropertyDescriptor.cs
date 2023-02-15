using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

public interface IOGraphNodePropertyDescriptor
{

    IOGraphNodePropertyDescriptor HasType<TType>() where TType : IOGraphType, new();

    IOGraphNodePropertyDescriptor HasResolver(IOGraphTypeResolver resolver);

    IOGraphNodePropertyDescriptor HasResolver(OGraphTypeResolver resolver);
}

/// <summary>
/// 
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IOGraphNodePropertyDescriptor<T>
{
    /// <summary>
    /// Overrides the name of the current property.
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    IOGraphNodePropertyDescriptor<T> HasName(Name name);
    /// <summary>
    /// Overrides the OGraph type for the current property.
    /// </summary>
    /// <typeparam name="TType"></typeparam>
    /// <returns></returns>
    IOGraphNodePropertyDescriptor<T> HasType<TType>() where TType : IOGraphType, new();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="resolver"></param>
    /// <returns></returns>
    IOGraphNodePropertyDescriptor<T> HasResolver(IOGraphTypeResolver resolver);

    /// <summary>
    /// Overrides or provides a Type Resolver for the current property.
    /// </summary>
    /// <param name="resolver"></param>
    /// <returns></returns>
    IOGraphNodePropertyDescriptor<T> HasResolver(OGraphTypeResolver<T> resolver);

}
