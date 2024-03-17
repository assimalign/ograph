using System;
using System.Linq;
using System.Collections.Generic;

namespace Assimalign.OGraph.Gdm;

using Assimalign.OGraph.Gdm.Internal;

/// <summary>
/// 
/// </summary>
public sealed class OGraphGdmBuilder : IOGraphGdmBuilder
{
    private readonly Gdm model;
    private readonly GdmValidator validator;


    private readonly IList<Action<Gdm>> beforeBuild;
    private readonly IList<Action<Gdm>> onBuild;
    private readonly IList<Action<Gdm>> afterBuild;

    OGraphGdmBuilder(Label label)
    {
        this.beforeBuild = new List<Action<Gdm>>();
        this.onBuild = new List<Action<Gdm>>();
        this.afterBuild = new List<Action<Gdm>>();
        this.validator = new();
        this.model = new()
        {
            Label = label
        };

        afterBuild.Add(TryResolveTypeReferences);
    }


    IOGraphGdmBuilder IOGraphGdmBuilder.BeforeBuild(Action<IOGraphGdm> configure)
    {
        if (configure is null)
        {
            GdmThrowHelper.ThrowArgumentNullException(nameof(configure));
        }

        beforeBuild.Add(configure);

        return this;
    }
    IOGraphGdmBuilder IOGraphGdmBuilder.AfterBuild(Action<IOGraphGdm> configure)
    {
        if (configure is null)
        {
            GdmThrowHelper.ThrowArgumentNullException(nameof(configure));
        }

        afterBuild.Add(configure);

        return this;
    }
    IOGraphGdmBuilder IOGraphGdmBuilder.AddType<TGdmType>()
    {
        return (this as IOGraphGdmBuilder).AddType(new TGdmType());
    }
    IOGraphGdmBuilder IOGraphGdmBuilder.AddType(IOGraphGdmType type)
    {
        if (type is null)
        {
            GdmThrowHelper.ThrowArgumentNullException(nameof(type));
        }

        var elements = model.Elements;
        var hasType = elements.OfType<IOGraphGdmType>().Any(p => p.Label == type.Label);

        if (!hasType)
        {
            model.Elements.Add(type);

            switch (type)
            {
                case IOGraphGdmComplexType complex:
                    {

                        break;
                    }
                case IOGraphGdmCollectionType collection:
                    {


                        break;
                    }
            }

            if (type is IOGraphGdmComplexType complex) // This accounts for GDM Entity Types as well
            {
                var props = new GdmPropertyCollection();
                var properties = complex.Properties;

                foreach (var property in properties)
                {
                    var prop = GdmProperty.Wrap(property);
                    var propertyType = prop?.Type?.Definition;

                    if (propertyType is null)
                    {
                        props.Add(prop);

                        // If the property type is null this could be a result of a shared type that is configured post build.
                        continue;
                    }
                    
                    var propType = elements.OfType<IOGraphGdmType>().SingleOrDefault(p => p.Label == propertyType.Label);

                    if (propType is null)
                    {
                        (this as IOGraphGdmBuilder).AddType(propertyType);
                    }
                    else
                    {
                        prop.Type = new GdmTypeReference()
                        {
                            Definition = propType
                        };
                    }

                    props.Add(prop);
                }

                complex.Properties.Clear();

                foreach (var item in props)
                {
                    complex.Properties.Add(item);
                    model.Elements.Add(item);
                }
            }
            if (type is IOGraphGdmCollectionType collection)
            {

            }
        }
        return this;
    }
    IOGraphGdmBuilder IOGraphGdmBuilder.AddVertex<TVertex>()
    {
        return (this as IOGraphGdmBuilder).AddVertex(new TVertex());
    }
    IOGraphGdmBuilder IOGraphGdmBuilder.AddVertex(IOGraphGdmVertex vertex)
    {
        if (vertex is null)
        {
            GdmThrowHelper.ThrowArgumentNullException(nameof(vertex));
        }

        var hasVertex = model.Elements.OfType<IOGraphGdmVertex>().Any(p=>p.Label == vertex.Label);

        if (hasVertex)
        {
            throw new Exception(); // Duplicate vertex
        }

        var type = vertex.Type.Definition;

        if (type is null)
        {
            throw new Exception();
        }

        (this as IOGraphGdmBuilder).AddType(type);

        model.Elements.Add(vertex);

        return this;
    }
    IOGraphGdmBuilder IOGraphGdmBuilder.AddEdge<TSource, TTarget>(Label label)
    {
        throw new NotImplementedException();
    }
    IOGraphGdm IOGraphGdmBuilder.Build()
    {
        OnBuild(beforeBuild);
        OnBuild(afterBuild);

        var result = validator.Validate(model);

        if (!result.IsValid)
        {
            throw result.ToException();
        }

        //(model.Elements as GdmElementCollection)!.IsReadOnly = true;

        return model;
    }

    private void OnBuild(IList<Action<Gdm>> actions)
    {
        foreach (var action in actions)
        {
            action.Invoke(model);
        }
    }

    #region Static Memebers
    /// <summary>
    /// Creates a graph data model.
    /// </summary>
    /// <param name="label">The Graph Model name.</param>
    /// <param name="configure"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static IOGraphGdm Create(Label label, Action<IOGraphGdmBuilder> configure)
    {
        if (configure is null)
        {
            GdmThrowHelper.ThrowArgumentNullException(nameof(configure));
        }

        IOGraphGdmBuilder builder = new OGraphGdmBuilder(label);

        configure.Invoke(builder);

        return builder.Build();
    }
    /// <summary>
    /// Creates a graph data model builder.
    /// </summary>
    /// <param name="label"></param>
    /// <returns></returns>
    public static IOGraphGdmBuilder Create(Label label)
    {
        return new OGraphGdmBuilder(label);
    }
    #endregion


    private static void TryResolveTypeReferences(Gdm model)
    {
        foreach (var property in model.GetGdmProperties().Where(p => p is GdmProperty).Cast<GdmProperty>())
        {
            // 1. Check if type has been set
            if (property.Type is null)
            {
                // 2. Find the first assignable to runtime property, if any.
                var gdmType = model.GetGdmTypes().FirstOrDefault(p =>
                    p.RuntimeType!.IsAssignableTo(property.PropertyInfo.PropertyType));

                if (gdmType is not null)
                {
                    property.Type = new GdmTypeReference()
                    {
                        Definition = gdmType
                    };
                }
                else
                {

                }
            }
        }
    }
}