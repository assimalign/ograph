using System;
using System.Diagnostics.CodeAnalysis;

namespace Assimalign.OGraph.Gdm;

using Assimalign.OGraph.Gdm.Internal;

public static class OGraphGdmVertexDescriptorExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="descriptor"></param>
    /// <param name="configure"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static IOGraphGdmVertexDescriptor<T> HasType<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties)] T>(
        this IOGraphGdmVertexDescriptor<T> descriptor,
        Action<IOGraphGdmEntityTypeDescriptor<T>> configure)
        where T : class, new()
    {
        if (descriptor is null)
        {
            GdmThrowHelper.ThrowArgumentNullException(nameof(descriptor));
        }
        if (configure is null)
        {
            GdmThrowHelper.ThrowArgumentNullException(nameof(configure));
        }
        return descriptor.HasType(
            GdmEntityType<T>.Create(
                configure));
    }
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="descriptor"></param>
    /// <param name="configure"></param>
    /// <returns></returns>
    public static IOGraphGdmVertexDescriptor HasType<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties)] T>(
        this IOGraphGdmVertexDescriptor descriptor, 
        Action<IOGraphGdmEntityTypeDescriptor<T>> configure) where T : class, new()
    {
        if (descriptor is null)
        {
            GdmThrowHelper.ThrowArgumentNullException(nameof(descriptor));
        }
        if (configure is null)
        {
            GdmThrowHelper.ThrowArgumentNullException(nameof(configure));
        }
        return descriptor.HasType(
            GdmEntityType<T>.Create(
                configure));
    }
}
