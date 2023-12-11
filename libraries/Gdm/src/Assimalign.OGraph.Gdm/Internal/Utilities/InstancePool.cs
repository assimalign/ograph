using System;
using System.Collections.Concurrent;

namespace Assimalign.OGraph.Gdm.Internal;

internal class InstancePool
{
    private readonly ConcurrentQueue<Instance> instances = new();

    public void Return(Instance instance)
    {
        instances.Enqueue(instance);
    }

    public Instance Rent()
    {
        Instance instance = default!;

        if (instances.TryDequeue(out instance!))
        {
            return instance;
        }

        instance = new Instance(this);

        instances.Enqueue(instance);

        return instance;
    }
}
