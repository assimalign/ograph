using System;

namespace Assimalign.OGraph.Gdm;

public interface IOGraphGdmFactoryBuilder
{
    IOGraphGdmFactoryBuilder Configure(Label label, Action<IOGraphGdmBuilder> configure);
    IOGraphGdmFactory Build();
}
