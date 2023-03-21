using System;
using System.Threading;

namespace Assimalign.OGraph.Internal;

internal sealed class ObjectPoolDisposable<T> : ObjectPoolDefault<T>, IDisposable where T : class
{
    private volatile bool isDisposed;

    public ObjectPoolDisposable(IObjectPoolPolicy<T> policy)
        : base(policy)
    {
    }

    public ObjectPoolDisposable(IObjectPoolPolicy<T> policy, int maximumRetained)
        : base(policy, maximumRetained)
    {
    }

    public override T Get()
    {
        if (isDisposed)
        {
            ThrowObjectDisposedException();
        }

        return base.Get();

        void ThrowObjectDisposedException()
        {
            throw new ObjectDisposedException(GetType().Name);
        }
    }

    public override void Return(T instance)
    {
        // When the pool is disposed or the obj is not returned to the pool, dispose it
        if (isDisposed || !ReturnCore(instance))
        {
            DisposeItem(instance);
        }
    }

    private bool ReturnCore(T instance)
    {
        bool returnedTooPool = false;

        if (isDefaultPolicy || (fastPolicy?.Return(instance) ?? policy.Return(instance)))
        {
            if (firstElement == null && Interlocked.CompareExchange(ref firstElement, instance, null) == null)
            {
                returnedTooPool = true;
            }
            else
            {
                var items = elements;
                for (var i = 0; i < items.Length && !(returnedTooPool = Interlocked.CompareExchange(ref items[i].Element, instance, null) == null); i++)
                {
                }
            }
        }

        return returnedTooPool;
    }

    public void Dispose()
    {
        isDisposed = true;

        DisposeItem(firstElement);
        firstElement = null;

        ObjectWrapper[] items = elements;
        for (var i = 0; i < items.Length; i++)
        {
            DisposeItem(items[i].Element);
            items[i].Element = null;
        }
    }

    private static void DisposeItem(T? item)
    {
        if (item is IDisposable disposable)
        {
            disposable.Dispose();
        }
    }
}
