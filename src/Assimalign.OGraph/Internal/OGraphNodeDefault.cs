using System;

namespace Assimalign.OGraph.Internal;

internal class OGraphNodeDefault<T> : OGraphNode<T>
{
    private readonly Action<IOGraphNodeDescriptor<T>> configure;

    public OGraphNodeDefault(Action<IOGraphNodeDescriptor<T>> configure)
    {
        this.configure = configure;
    }


    public override void Configure(IOGraphNodeDescriptor<T> descriptor)
    {
        if (descriptor is null)
        {
            throw new ArgumentNullException(nameof(descriptor));
        }

        configure.Invoke(descriptor);
    }
}
