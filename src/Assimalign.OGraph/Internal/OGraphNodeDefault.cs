using System;

namespace Assimalign.OGraph.Internal;

internal class OGraphNodeDefault<T> : OGraphNode
{

    public void Configure(Action<IOGraphNodeDescriptor<T>> configure)
    {
        if (configure is null)
        {
            throw new ArgumentNullException(nameof(configure));
        }

        var descriptor = new OGraphNodeDescriptor<T>(this);

        configure.Invoke(descriptor);
    }

    protected override void Configure(IOGraphNodeDescriptor descriptor)
    {
        throw new NotImplementedException();
    }
}
