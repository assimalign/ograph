using System;

namespace Assimalign.OGraph.Gdm;

public interface IOGraphGdmFactoryBuilder
{
    IOGraphGdmFactoryBuilder Configure(Action<IOGraphGdmBuilder> configure);
    IOGraphGdmFactory Build();
}
