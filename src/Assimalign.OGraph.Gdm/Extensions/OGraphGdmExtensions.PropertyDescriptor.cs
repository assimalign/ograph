using System;
using System.Collections;
using System.Collections.Generic;

namespace Assimalign.OGraph.Gdm;

public static class OGraphGdmPropertyDescriptorExtensions
{
    /// <summary>
    /// A fluent method for binding a complex type property being described.
    /// </summary>
    /// <typeparam name="T">The property type being configured.</typeparam>
    /// <param name="descriptor">The property descriptor.</param>
    /// <param name="configure"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static IOGraphGdmPropertyDescriptor<T?> UseType<T>(
        this IOGraphGdmPropertyDescriptor<T?> descriptor,
        Action<IOGraphGdmComplexTypeDescriptor<T>> configure) where T : class, new()
    {
        if (configure is null)
        {
            throw new ArgumentNullException(nameof(configure));
        }

        return descriptor.UseType(GdmComplexType<T>.Create(configure));
    }
    /// <summary>
    ///  A fluent method for binding a complex type within a collection property being described.
    /// </summary>
    /// <typeparam name="T">The property type being configured.</typeparam>
    /// <param name="descriptor">The property descriptor.</param>
    /// <param name="configure"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static IOGraphGdmPropertyDescriptor<IEnumerable<T>?> UseType<T>(
        this IOGraphGdmPropertyDescriptor<IEnumerable<T>?> descriptor,
        Action<IOGraphGdmComplexTypeDescriptor<T>> configure) where T : class, new()
    {
        if (configure is null)
        {
            throw new ArgumentNullException(nameof(configure));
        }

        return descriptor.UseType(new GdmCollectionType<GdmComplexType<T>>(
            GdmComplexType<T>.Create(configure)));
    }
}
