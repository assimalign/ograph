using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Internal;

/// <summary>
/// A provider of <see cref="ObjectPool{T}"/> instances.
/// </summary>
public abstract class ObjectPoolProvider
{
    /// <summary>
    /// Creates an <see cref="ObjectPool"/>.
    /// </summary>
    /// <typeparam name="T">The type to create a pool for.</typeparam>
    public ObjectPool<T> Create<T>() where T : class, new()
    {
        return Create<T>(new ObjectPoolPolicyDefault<T>());
    }

    /// <summary>
    /// Creates an <see cref="ObjectPool"/> with the given <see cref="IObjectPoolPolicy{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type to create a pool for.</typeparam>
    public abstract ObjectPool<T> Create<T>(IObjectPoolPolicy<T> policy) where T : class;
}
