using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Assimalign.OGraph.Gdm;

using Elements;
using Internal;


public static class OGraphGdmDescriptorExtensions
{
    #region Descriptor Extensions: Property



    /// <summary>
    /// A fluent method for binding a complex type property being described.
    /// </summary>
    /// <typeparam name="T">The property type being configured.</typeparam>
    /// <param name="descriptor">The property descriptor.</param>
    /// <param name="configure"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static IOGraphGdmPropertyDescriptor<T?> UseType<[
        DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] T>(
        this IOGraphGdmPropertyDescriptor<T?> descriptor,
        Action<IOGraphGdmComplexTypeDescriptor<T>> configure) where T : class, new()
    {
        ThrowHelper.ThrowIfNull(descriptor, nameof(descriptor));
        ThrowHelper.ThrowIfNull(configure, nameof(configure));

        return descriptor.UseType(graph =>
        {

        });
    }

    /// <summary>
    ///  A fluent method for binding a complex type within a collection property being described.
    /// </summary>
    /// <typeparam name="T">The property type being configured.</typeparam>
    /// <param name="descriptor">The property descriptor.</param>
    /// <param name="configure"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static IOGraphGdmPropertyDescriptor<IEnumerable<T>?> UseType<[
        DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] T>(
        this IOGraphGdmPropertyDescriptor<IEnumerable<T>?> descriptor,
        Action<IOGraphGdmComplexTypeDescriptor<T>> configure) where T : class, new()
    {
        ThrowHelper.ThrowIfNull(descriptor, nameof(descriptor));
        ThrowHelper.ThrowIfNull(configure, nameof(configure));

        return descriptor.UseType(graph =>
        {

        });

        //return descriptor.UseType(
        //    new GdmListType<T>(
        //        GdmComplexType<T>.Create(
        //            configure)));
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
    public static IOGraphGdmPropertyDescriptor<IEnumerable<T>?> UseType<[
        DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] T>(
        this IOGraphGdmPropertyDescriptor<IEnumerable<T>?> descriptor,
        GdmLabel label,
        Action<IOGraphGdmComplexTypeDescriptor<T>> configure) where T : class, new()
    {
        ThrowHelper.ThrowIfNull(descriptor, nameof(descriptor));
        ThrowHelper.ThrowIfNull(configure, nameof(configure));

        return descriptor.UseType(graph =>
        {

        });

        //return descriptor.UseType(
        //    new GdmListType<T>(
        //        GdmComplexType<T>.Create(configure))
        //    {
        //        label = label
        //    });
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="descriptor"></param>
    /// <param name="configure"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static IOGraphGdmPropertyDescriptor<T[]?> UseType<[
        DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] T>(
        this IOGraphGdmPropertyDescriptor<T[]?> descriptor,
        Action<IOGraphGdmComplexTypeDescriptor<T>> configure)
        where T : class, new()
    {
        ThrowHelper.ThrowIfNull(descriptor, nameof(descriptor));
        ThrowHelper.ThrowIfNull(configure, nameof(configure));

        return descriptor.UseType(graph =>
        {

        });

        //return descriptor.UseType(
        //    new GdmArrayType<T>(
        //        GdmComplexType<T>.Create(
        //            configure)));
    }

    #endregion

    #region Descriptor Extensions: Vertex

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="descriptor"></param>
    /// <param name="configure"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static IOGraphGdmVertexDescriptor<T> HasEntityType<[
        DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] T>(
        this IOGraphGdmVertexDescriptor<T> descriptor,
        Action<IOGraphGdmEntityTypeDescriptor<T>> configure)
        where T : class, new()
    {
        ThrowHelper.ThrowIfNull(descriptor, nameof(descriptor));
        ThrowHelper.ThrowIfNull(configure, nameof(configure));

        return descriptor.HasEntityType(
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
    public static IOGraphGdmVertexDescriptor HasType<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] T>(
        this IOGraphGdmVertexDescriptor descriptor,
        Action<IOGraphGdmEntityTypeDescriptor<T>> configure) where T : class, new()
    {
        ThrowHelper.ThrowIfNull(descriptor, nameof(descriptor));
        ThrowHelper.ThrowIfNull(configure, nameof(configure));

        return descriptor.HasEntityType(
            GdmEntityType<T>.Create(
                configure));
    }

    #endregion

    #region Descriptor Extensions: Graph

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="descriptor"></param>
    /// <param name="label"></param>
    /// <param name="configure"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static IOGraphGdmGraphDescriptor AddVertex<[
        DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] T>(
        this IOGraphGdmGraphDescriptor descriptor,
        GdmLabel label,
        Action<IOGraphGdmEntityTypeDescriptor<T>> configure)
        where T : class, new()
    {
        ThrowHelper.ThrowIfNull(descriptor, nameof(descriptor));
        ThrowHelper.ThrowIfNull(configure, nameof(configure));

        return descriptor.AddVertex(graph =>
        {
            var type = new GdmEntityType<T>();
            var descriptor = new GdmEntityTypeDescriptor<T>(type, graph);
            return new GdmVertex<T>()
            {
                Label = label,
                Graph = graph,
                Type = new GdmEntityType<T>(configure)
                {
                    Graph = graph,
                }
            };
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
    public static IOGraphGdmGraphDescriptor AddVertex<T>(
        this IOGraphGdmGraphDescriptor descriptor,
        Action<IOGraphGdmVertexDescriptor<T>> configure)
        where T : class, new()
    {
        ThrowHelper.ThrowIfNull(descriptor, nameof(descriptor));
        ThrowHelper.ThrowIfNull(configure, nameof(configure));

        return descriptor.AddVertex(graph =>
        {
            return new GdmVertex<T>(configure)
            {
                Graph = graph
            };
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
    public static IOGraphGdmGraphDescriptor AddComplexType<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties)] T>(this IOGraphGdmGraphDescriptor descriptor, Action<IOGraphGdmComplexTypeDescriptor<T>> configure) where T : class, new()
    {
        ThrowHelper.ThrowIfNull(descriptor, nameof(descriptor));
        ThrowHelper.ThrowIfNull(configure, nameof(configure));

        return descriptor.AddType(GdmComplexType<T>.Create(configure));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="descriptor"></param>
    /// <param name="configure"></param>
    /// <returns></returns>
    public static IOGraphGdmGraphDescriptor AddEntityType<
        [DynamicallyAccessedMembers(
        DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | 
        DynamicallyAccessedMemberTypes.PublicProperties)] T>(
        this IOGraphGdmGraphDescriptor descriptor, 
        Action<IOGraphGdmEntityTypeDescriptor<T>> configure) where T : class, new()
    {
        ThrowHelper.ThrowIfNull(descriptor, nameof(descriptor));
        ThrowHelper.ThrowIfNull(configure, nameof(configure));

        return descriptor.AddType(GdmEntityType<T>.Create(configure));
    }
    #endregion
}
