using System;
using System.Linq;
using System.Diagnostics.CodeAnalysis;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm;

using Assimalign.OGraph.Gdm.Internal;

public static class OGraphGdmBuilderExtensions
{
    /// <summary>
    /// Will convert all properties in the GDM to camal case.
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static IOGraphGdmBuilder SetPropertyNamesToCamalCase(this IOGraphGdmBuilder builder)
    {
        if (builder is null)
        {
            GdmThrowHelper.ThrowArgumentNullException(nameof(builder));
        }
        return builder.AfterBuild(model =>
        {
            foreach (var property in model.GetGdmProperties().Where(p=> p is GdmProperty).Cast<GdmProperty>())
            {
                property.Label = property.Label.ToCamalCase();
            }
        });
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="builder"></param>
    /// <param name="configure"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static IOGraphGdmBuilder AddVertex<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties)] T>(this IOGraphGdmBuilder builder, Action<IOGraphGdmEntityTypeDescriptor<T>> configure)
        where T : class, new()
    {
        if (builder is null)
        {
            GdmThrowHelper.ThrowArgumentNullException(nameof(builder));
        }
        if (configure is null)
        {
            GdmThrowHelper.ThrowArgumentNullException(nameof(configure));
        }
        Action<IOGraphGdmVertexDescriptor<T>> action = vertex =>
        {
            vertex.HasLabel(typeof(T).Name);
            vertex.HasType(GdmEntityType<T>.Create(configure));
        };
        return builder.AddVertex<T>(action);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="builder"></param>
    /// <param name="label">The label of the vertex</param>
    /// <param name="configure"></param>
    /// <returns></returns>
    public static IOGraphGdmBuilder AddVertex<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties)] T>(this IOGraphGdmBuilder builder, Label label, Action<IOGraphGdmEntityTypeDescriptor<T>> configure)
        where T : class, new()
    {
        if (builder is null)
        {
            GdmThrowHelper.ThrowArgumentNullException(nameof(builder));
        }
        if (configure is null)
        {
            GdmThrowHelper.ThrowArgumentNullException(nameof(configure));
        }
        Action<IOGraphGdmVertexDescriptor<T>> action = vertex =>
        {
            vertex.HasLabel(label);
            vertex.HasType(GdmEntityType<T>.Create(configure));
        };
        return builder.AddVertex<T>(action);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="builder"></param>
    /// <param name="configure"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static IOGraphGdmBuilder AddVertex<T>(this IOGraphGdmBuilder builder, Action<IOGraphGdmVertexDescriptor<T>> configure) 
        where T : class, new()
    {
        if (builder is null)
        {
            GdmThrowHelper.ThrowArgumentNullException(nameof(builder));
        }
        if (configure is null)
        {
            GdmThrowHelper.ThrowArgumentNullException(nameof(configure));
        }
        return builder.AddVertex(GdmVertex<T>.Create(configure));
    }
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="builder"></param>
    /// <param name="configure"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static IOGraphGdmBuilder AddType<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties)] T>(this IOGraphGdmBuilder builder, Action<IOGraphGdmComplexTypeDescriptor<T>> configure) where T : class, new()
    {
        if (builder is null)
        {
            GdmThrowHelper.ThrowArgumentNullException(nameof(builder));
        }
        if (configure is null)
        {
            GdmThrowHelper.ThrowArgumentNullException(nameof(configure));
        }
        return builder.AddType(GdmComplexType<T>.Create(configure));
    }
}