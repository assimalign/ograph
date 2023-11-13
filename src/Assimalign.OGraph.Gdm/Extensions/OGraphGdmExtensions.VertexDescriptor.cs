using System;

namespace Assimalign.OGraph.Gdm;

using Internal;

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
    public static IOGraphGdmVertexDescriptor<T> HasType<T>(
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
        return descriptor.HasType(GdmEntityType<T>.Create(configure));
    }
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="descriptor"></param>
    /// <param name="configure"></param>
    /// <returns></returns>
    public static IOGraphGdmVertexDescriptor HasType<T>(
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
        return descriptor.HasType(GdmEntityType<T>.Create(configure));
    }
}
