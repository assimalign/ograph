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

    public static IOGraphGdmBuilder ConfigureOptions(this IOGraphGdmBuilder builder , Action<OGraphGdmBuilderOptions> configure)
    {
        var options = new OGraphGdmBuilderOptions();

        configure.Invoke(options);


        return default;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static IOGraphGdmBuilder SetAllPropertiesToCamalCase(this IOGraphGdmBuilder builder)
    {
        return builder.AfterBuild(model =>
        {
            foreach (var complexType in model.GetGdmComplexTypes())
            {
                // Copy the current properties to a new collection
                var properties = new List<IOGraphGdmProperty>(complexType.Properties);

                // Clear out the existing properties
                complexType.Properties.Clear();

                foreach (var property in complexType.Properties)
                {
                    var prop = GdmProperty.Wrap(property);

                    prop.Label = prop.Label.ToCamalCase();

                    complexType.Properties.Add(prop);
                }
            }
        });
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
            ThrowHelper.ThrowArgumentNullException(nameof(builder));
        }
        if (configure is null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(configure));
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
            ThrowHelper.ThrowArgumentNullException(nameof(builder));
        }
        if (configure is null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(configure));
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
    public static IOGraphGdmBuilder AddComplexType<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties)] T>(this IOGraphGdmBuilder builder, Action<IOGraphGdmComplexTypeDescriptor<T>> configure) where T : class, new()
    {
        if (builder is null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(builder));
        }
        if (configure is null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(configure));
        }
        return builder.AddType(GdmComplexType<T>.Create(configure));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="builder"></param>
    /// <param name="configure"></param>
    /// <returns></returns>
    public static IOGraphGdmBuilder AddEntityType<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties)] T>(this IOGraphGdmBuilder builder, Action<IOGraphGdmEntityTypeDescriptor<T>> configure) where T : class, new()
    {
        if (builder is null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(builder));
        }
        if (configure is null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(configure));
        }
        return builder.AddType(GdmEntityType<T>.Create(configure));
    }
}