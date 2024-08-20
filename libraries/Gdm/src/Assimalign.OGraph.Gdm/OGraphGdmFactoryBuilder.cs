using System;
using System.Collections.Generic;

namespace Assimalign.OGraph.Gdm;

using Assimalign.OGraph.Gdm.Internal;

public sealed class OGraphGdmFactoryBuilder : IOGraphGdmFactoryBuilder
{
    private GdmBuilderStrategy strategy = GdmBuilderStrategy.Explicit;

    private readonly IList<IOGraphGdm> models;
    private readonly IDictionary<Label, IList<Action<IOGraphGdmBuilder>>> actions;

    public OGraphGdmFactoryBuilder()
    {
        models = new List<IOGraphGdm>();
        actions = new Dictionary<Label, IList<Action<IOGraphGdmBuilder>>>();
    }


    public IOGraphGdmFactoryBuilder UseStrategy(GdmBuilderStrategy strategy)
    {
        this.strategy = strategy;
        return this;
    }


    public IOGraphGdmFactoryBuilder Configure(Label label, Action<IOGraphGdmBuilder> configure)
    {
        if (configure is null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(configure));
        }
        if (actions.TryGetValue(label, out var builds))
        {
            builds.Add(configure);
        }
        else
        {
            actions[label] = new List<Action<IOGraphGdmBuilder>>() { configure };
        }

        return this;
    }

    public IOGraphGdmFactory Build()
    {
        foreach (var (key, value) in actions)
        {
            var builder = OGraphGdmBuilder.Create(key);

            foreach (var action in value)
            {
                action.Invoke(builder);
            }

            models.Add(builder.Build());
        }

        return new GdmFactory(models);
    }
}
