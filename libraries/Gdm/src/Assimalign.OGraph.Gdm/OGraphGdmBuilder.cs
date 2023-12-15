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

    private readonly IList<Action<Gdm>> onBeforeBuild;
    private readonly IList<Action<Gdm>> onAfterBuild;

    private OGraphGdmBuilder(Label label)
    {
        this.onBeforeBuild = new List<Action<Gdm>>();
        this.onAfterBuild = new List<Action<Gdm>>();
        this.validator = new();
        this.model = new()
        {
            Label = label
        };

        onAfterBuild.Add(TryResolveTypeReferences);
    }
    IOGraphGdmBuilder IOGraphGdmBuilder.BeforeBuild(Action<IOGraphGdm> configure)
    {
        if (configure is null)
        {
            GdmThrowHelper.ThrowArgumentNullException(nameof(configure));
        }

        onBeforeBuild.Add(configure);

        return this;
    }
    IOGraphGdmBuilder IOGraphGdmBuilder.AfterBuild(Action<IOGraphGdm> configure)
    {
        if (configure is null)
        {
            GdmThrowHelper.ThrowArgumentNullException(nameof(configure));
        }

        onAfterBuild.Add(configure);

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

        model.Elements.Add(type);

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

        model.Elements.Add(vertex);

        return this;
    }
    IOGraphGdm IOGraphGdmBuilder.Build()
    {
        OnBuild(onBeforeBuild);
        OnBuild(onAfterBuild);

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