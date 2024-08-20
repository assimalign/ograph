using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Assimalign.OGraph.Gdm;

using Assimalign.OGraph.Gdm.Internal;

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
    public static IOGraphGdmPropertyDescriptor<T?> UseType<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] T>(
        this IOGraphGdmPropertyDescriptor<T?> descriptor,
        Action<IOGraphGdmComplexTypeDescriptor<T>> configure) where T : class, new()
    {
        if (descriptor is null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(descriptor));
        }
        if (configure is null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(configure));
        }
        return descriptor.UseType(
            GdmComplexType<T>.Create(
                configure));
    }
    
    /// <summary>
    ///  A fluent method for binding a complex type within a collection property being described.
    /// </summary>
    /// <typeparam name="T">The property type being configured.</typeparam>
    /// <param name="descriptor">The property descriptor.</param>
    /// <param name="configure"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static IOGraphGdmPropertyDescriptor<IEnumerable<T>?> UseType<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] T>(
        this IOGraphGdmPropertyDescriptor<IEnumerable<T>?> descriptor,
        Action<IOGraphGdmComplexTypeDescriptor<T>> configure) where T : class, new()
    {
        if (descriptor is null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(descriptor));
        }
        if (configure is null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(configure));
        }
        return descriptor.UseType(
            new GdmListType<T>(
                GdmComplexType<T>.Create(
                    configure)));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="descriptor"></param>
    /// <param name="label"></param>
    /// <param name="configure"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static IOGraphGdmPropertyDescriptor<IEnumerable<T>?> UseType<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] T>(
        this IOGraphGdmPropertyDescriptor<IEnumerable<T>?> descriptor,
        Label label,
        Action<IOGraphGdmComplexTypeDescriptor<T>> configure) where T : class, new()
    {
        if (descriptor is null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(descriptor));
        }
        if (configure is null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(configure));
        }
        return descriptor.UseType(
            new GdmListType<T>(
                GdmComplexType<T>.Create(configure))
                {
                    label = label
                });
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="descriptor"></param>
    /// <param name="configure"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static IOGraphGdmPropertyDescriptor<T[]?> UseType<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] T>(
        this IOGraphGdmPropertyDescriptor<T[]?> descriptor,
        Action<IOGraphGdmComplexTypeDescriptor<T>> configure) 
        where T : class, new()
    {
        if (descriptor is null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(descriptor));
        }
        if (configure is null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(configure));
        }
        return descriptor.UseType(
            new GdmArrayType<T>(
                GdmComplexType<T>.Create(
                    configure)));
    }
}
