using System;
using System.Linq;
using System.Collections.Generic;

namespace Assimalign.OGraph.Gdm;

using Internal;

/// <summary>
/// 
/// </summary>
public sealed class OGraphGdmBuilder : IOGraphGdmBuilder
{
    private readonly Gdm model;
    private readonly GdmValidator validator;
    private readonly IList<Action<Gdm>> onTypeAdd;
    private readonly IList<Action<Gdm>> onVertexAdd;

    private OGraphGdmBuilder(Label label)
    {
        this.onTypeAdd = new List<Action<Gdm>>();
        this.onVertexAdd = new List<Action<Gdm>>();
        this.validator = new();
        this.model = new()
        {
            Label = label
        };
    }
    IOGraphGdmBuilder IOGraphGdmBuilder.AddType<T>(Action<IOGraphGdmComplexTypeDescriptor<T>> configure)
    {
        return (this as IOGraphGdmBuilder).AddType(GdmComplexType<T>.Create(configure));
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
        onTypeAdd.Add(gdm =>
        {
            gdm.Elements.Add(type);
        });
        return this;
    }
    IOGraphGdmBuilder IOGraphGdmBuilder.AddVertex<T>(Action<IOGraphGdmEntityTypeDescriptor<T>> configure)
    {
        if (configure is null)
        {
            throw new ArgumentNullException(nameof(configure));
        }
        var vertex = new GdmVertex<GdmEntityType<T>>()
        {
            type = new GdmTypeReference()
            {
                Definition = GdmEntityType<T>.Create(configure)
            }
        };
        return (this as IOGraphGdmBuilder).AddVertex(vertex);
    }
    IOGraphGdmBuilder IOGraphGdmBuilder.AddVertex(Action<IOGraphGdmVertexDescriptor> configure)
    {
        return (this as IOGraphGdmBuilder).AddVertex(GdmVertex.Create(configure));
    }
    IOGraphGdmBuilder IOGraphGdmBuilder.AddVertex<T>(Action<IOGraphGdmVertexDescriptor<T>> configure)
    {
        return (this as IOGraphGdmBuilder).AddVertex(GdmVertex<T>.Create(configure));
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
        onVertexAdd.Add(gdm =>
        {
            // Check for types already added and replace in properties
            foreach (var property in vertex.GetProperties())
            {
                if (property is GdmProperty ip)
                {
                    var type = gdm.GetGdmTypes().FirstOrDefault(p =>
                    {
                        if (p.RuntimeType is null)
                        {
                            return false;
                        }
                        return p.RuntimeType.IsAssignableTo(ip.PropertyInfo.PropertyType);
                    });
                    if (type is not null)
                    {
                        ip.Type = new GdmTypeReference()
                        {
                            Definition = type
                        };
                    }
                }
            }

            gdm.Elements.Add(vertex);
        });
        return this;
    }
    IOGraphGdm IOGraphGdmBuilder.Build()
    {
        OnBuild(onTypeAdd);
        OnBuild(onVertexAdd);

        var result = validator.Validate(model);

        if (!result.IsValid)
        {
            throw result.ToException();
        }

        (model.Elements as GdmElementCollection)!.IsReadOnly = true;

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
}