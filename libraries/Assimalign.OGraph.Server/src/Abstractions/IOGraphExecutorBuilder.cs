using System;

namespace Assimalign.OGraph;

using Assimalign.OGraph.Gdm;

public interface IOGraphExecutorBuilder
{
    IOGraphExecutorBuilder ConfigureOptions(OGraphExecutorOptions options);
    IOGraphExecutorBuilder ConfigureOptions(Action<OGraphExecutorOptions> configure);
    IOGraphExecutorBuilder ConfigureModel(GdmName label, Action<IOGraphGdmBuilder> configure);
    IOGraphExecutorBuilder ConfigureApplication(Action<IOGraphApplicationBuilder> configure);
    IOGraphExecutor Build();
}