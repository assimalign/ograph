using System;

namespace Assimalign.OGraph.Internal;

/// <summary>
/// An <see cref="ObjectPoolProvider"/> that produces instances of
/// <see cref="ObjectPoolLeakTracking{T}"/>.
/// </summary>
public class LeakTrackingObjectPoolProvider : ObjectPoolProvider
{
    private readonly ObjectPoolProvider inner;

    /// <summary>
    /// Initializes a new instance of <see cref="LeakTrackingObjectPoolProvider"/>.
    /// </summary>
    /// <param name="inner">The <see cref="ObjectPoolProvider"/> to wrap.</param>
    public LeakTrackingObjectPoolProvider(ObjectPoolProvider inner)
    {
        if (inner == null)
        {
            throw new ArgumentNullException(nameof(inner));
        }

        this.inner = inner;
    }

    /// <inheritdoc/>
    public override ObjectPool<T> Create<T>(IObjectPoolPolicy<T> policy)
    {
        var inner = this.inner.Create<T>(policy);
        return new ObjectPoolLeakTracking<T>(inner);
    }
}
