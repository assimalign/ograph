using System;
using System.Collections.Generic;

namespace Assimalign.OGraph;

using Assimalign.OGraph.Gdm;
using Assimalign.OGraph.Internal;

public sealed class OGraphExecutorBuilder : IOGraphExecutorBuilder
{
    private readonly IList<IOGraphGdm> models;
    private readonly OGraphExecutorOptions options;
    private readonly IList<Action> onBuild;

    public OGraphExecutorBuilder()
    {
        models = new List<IOGraphGdm>();
        options = new OGraphExecutorOptions();
        onBuild = new List<Action>();
    }

    public IOGraphExecutorBuilder ConfigureApplication(Action<IOGraphApplicationBuilder> configure)
    {
        if (configure is null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(configure));
        }

        onBuild.Add(() =>
        {
            var builder = new ApplicationBuilder(models)
            {
                Options = options
            };

            configure.Invoke(builder);
        });

        return this;
    }

    public IOGraphExecutorBuilder ConfigureModel(Label label, Action<IOGraphGdmBuilder> configure)
    {
        if (configure is null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(configure));
        }
        models.Add(OGraphGdmBuilder.Create(label, configure));
        return this;
    }

    public IOGraphExecutorBuilder ConfigureOptions(Action<OGraphExecutorOptions> configure)
    {
        if (configure is null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(configure));
        }

        configure.Invoke(options);

        return this;
    }

    IOGraphExecutor IOGraphExecutorBuilder.Build()
    {
        foreach (var action in onBuild)
        {
            action();
        }

        return new Executor(models, options);
    }
}
