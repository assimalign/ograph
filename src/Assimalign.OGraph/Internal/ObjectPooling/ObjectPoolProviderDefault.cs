using System;

namespace Assimalign.OGraph.Internal;

/// <summary>
/// The default <see cref="ObjectPoolProvider"/>.
/// </summary>
public class ObjectPoolProviderDefault : ObjectPoolProvider
{
    /// <summary>
    /// The maximum number of objects to retain in the pool.
    /// </summary>
    public int MaximumRetained { get; set; } = Environment.ProcessorCount * 2;

    /// <inheritdoc/>
    public override ObjectPool<T> Create<T>(IObjectPoolPolicy<T> policy)
    {
        if (policy == null)
        {
            throw new ArgumentNullException(nameof(policy));
        }

        if (typeof(IDisposable).IsAssignableFrom(typeof(T)))
        {
            return new ObjectPoolDisposable<T>(policy, MaximumRetained);
        }

        return new ObjectPoolDefault<T>(policy, MaximumRetained);
    }
}
