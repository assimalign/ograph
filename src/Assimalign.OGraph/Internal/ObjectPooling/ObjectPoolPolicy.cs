namespace Assimalign.OGraph.Internal;

/// <summary>
/// A base type for <see cref="IObjectPoolPolicy{T}"/>.
/// </summary>
/// <typeparam name="T">The type of object which is being pooled.</typeparam>
public abstract class ObjectPoolPolicy<T> : IObjectPoolPolicy<T> where T : notnull
{
    /// <inheritdoc />
    public abstract T Create();

    /// <inheritdoc />
    public abstract bool Return(T instance);
}
