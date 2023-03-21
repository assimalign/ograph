using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Assimalign.OGraph.Internal;

/// <summary>
/// An <see cref="ObjectPool{T}"/> implementation that detects leaks in the use of the object pool.
/// <para>
/// A leak is produced if an object is leased from the pool but not returned before it is finalized.
/// An error is only produced in <c>Debug</c> builds.
/// This type is only recommended to be used for diagnostic builds.
/// </para>
/// </summary>
/// <typeparam name="T">The type of object which is being pooled.</typeparam>
public class ObjectPoolLeakTracking<T> : ObjectPool<T> where T : class
{
    private readonly ConditionalWeakTable<T, Tracker> trackers = new ConditionalWeakTable<T, Tracker>();
    private readonly ObjectPool<T> inner;

    /// <summary>
    /// Initializes a new instance of <see cref="ObjectPoolLeakTracking{T}"/>.
    /// </summary>
    /// <param name="inner">The <see cref="ObjectPool{T}"/> instance to track leaks in.</param>
    public ObjectPoolLeakTracking(ObjectPool<T> inner)
    {
        if (inner == null)
        {
            throw new ArgumentNullException(nameof(inner));
        }

        this.inner = inner;
    }

    /// <inheritdoc/>
    public override T Get()
    {
        var value = inner.Get();
        trackers.Add(value, new Tracker());
        return value;
    }

    /// <inheritdoc/>
    public override void Return(T instance)
    {
        if (trackers.TryGetValue(instance, out var tracker))
        {
            trackers.Remove(instance);
            tracker.Dispose();
        }

        inner.Return(instance);
    }

    private class Tracker : IDisposable
    {
        private readonly string stack;
        private bool disposed;

        public Tracker()
        {
            stack = Environment.StackTrace;
        }

        public void Dispose()
        {
            disposed = true;
            GC.SuppressFinalize(this);
        }

        ~Tracker()
        {
            if (!disposed && !Environment.HasShutdownStarted)
            {
                Debug.Fail($"{typeof(T).Name} was leaked. Created at: {Environment.NewLine}{stack}");
            }
        }
    }
}
