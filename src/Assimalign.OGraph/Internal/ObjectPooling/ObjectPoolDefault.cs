using System;
using System.Threading;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Assimalign.OGraph.Internal;

/// <summary>
/// Default implementation of <see cref="ObjectPool{T}"/>.
/// </summary>
/// <typeparam name="T">The type to pool objects for.</typeparam>
/// <remarks>This implementation keeps a cache of retained objects. This means that if objects are returned when the pool has already reached "maximumRetained" objects they will be available to be Garbage Collected.</remarks>
public class ObjectPoolDefault<T> : ObjectPool<T> where T : class
{
    private protected readonly ObjectWrapper[] elements;
    private protected readonly IObjectPoolPolicy<T> policy;
    private protected readonly bool isDefaultPolicy;
    private protected T? firstElement;

    // This class was introduced in 2.1 to avoid the interface call where possible
    private protected readonly ObjectPoolPolicy<T>? fastPolicy;

    /// <summary>
    /// Creates an instance of <see cref="ObjectPoolDefault{T}{T}"/>.
    /// </summary>
    /// <param name="policy">The pooling policy to use.</param>
    public ObjectPoolDefault(IObjectPoolPolicy<T> policy)
        : this(policy, Environment.ProcessorCount * 2)
    {
    }

    /// <summary>
    /// Creates an instance of <see cref="ObjectPoolDefault{T}"/>.
    /// </summary>
    /// <param name="policy">The pooling policy to use.</param>
    /// <param name="maximumRetained">The maximum number of objects to retain in the pool.</param>
    public ObjectPoolDefault(IObjectPoolPolicy<T> policy, int maximumRetained)
    {
        this.policy = policy ?? throw new ArgumentNullException(nameof(policy));
        this.fastPolicy = policy as ObjectPoolPolicy<T>;
        this.isDefaultPolicy = IsDefaultPolicy();

        // -1 due to _firstItem
        this.elements = new ObjectWrapper[maximumRetained - 1];

        bool IsDefaultPolicy()
        {
            var type = policy.GetType();

            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(ObjectPoolPolicyDefault<>);
        }
    }

    /// <inheritdoc />
    public override T Get()
    {
        var item = firstElement;
        if (item == null || Interlocked.CompareExchange(ref firstElement, null, item) != item)
        {
            var items = elements;
            for (var i = 0; i < items.Length; i++)
            {
                item = items[i].Element;
                if (item != null && Interlocked.CompareExchange(ref items[i].Element, null, item) == item)
                {
                    return item;
                }
            }

            item = Create();
        }

        return item;
    }

    // Non-inline to improve its code quality as uncommon path
    [MethodImpl(MethodImplOptions.NoInlining)]
    private T Create() => fastPolicy?.Create() ?? policy.Create();

    /// <inheritdoc />
    public override void Return(T instance)
    {
        if (isDefaultPolicy || (fastPolicy?.Return(instance) ?? policy.Return(instance)))
        {
            if (firstElement != null || Interlocked.CompareExchange(ref firstElement, instance, null) != null)
            {
                var items = elements;

                for (var i = 0; i < items.Length && Interlocked.CompareExchange(ref items[i].Element, instance, null) != null; ++i)
                {
                }
            }
        }
    }

    // PERF: the struct wrapper avoids array-covariance-checks from the runtime when assigning to elements of the array.
    [DebuggerDisplay("{Element}")]
    private protected struct ObjectWrapper
    {
        public T? Element;
    }
}
