using System;

namespace Assimalign.OGraph;

public interface IOGraphFactoryBuilder
{
    IOGraphFactoryBuilder AddGraph(IOGraph graph);
    IOGraphFactoryBuilder AddGraph(Action<IOGraphBuilder> configure);
    IOGraphFactory Build();
}
