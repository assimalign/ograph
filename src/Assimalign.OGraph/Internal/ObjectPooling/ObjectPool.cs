namespace Assimalign.OGraph.Internal;

/// <summary>
/// A pool of objects.
/// </summary>
/// <typeparam name="T">The type of objects to pool.</typeparam>
public abstract class ObjectPool<T> where T : class
{
    /// <summary>
    /// Gets an object from the pool if one is available, otherwise creates one.
    /// </summary>
    /// <returns>A <typeparamref name="T"/>.</returns>
    public abstract T Get();

    /// <summary>
    /// Return an object to the pool.
    /// </summary>
    /// <param name="instance">The object to add to the pool.</param>
    public abstract void Return(T instance);
}

/// <summary>
/// Methods for creating <see cref="ObjectPool{T}"/> instances.
/// </summary>
public static class ObjectPool
{
    /// <inheritdoc />
    public static ObjectPool<T> Create<T>(IObjectPoolPolicy<T>? policy = null) where T : class, new()
    {
        var provider = new ObjectPoolProviderDefault();
        return provider.Create(policy ?? new ObjectPoolPolicyDefault<T>());
    }
}
