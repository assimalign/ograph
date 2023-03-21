using System.Text;

namespace Assimalign.OGraph.Internal;

/// <summary>
/// A policy for pooling <see cref="StringBuilder"/> instances.
/// </summary>
public class PooledObjectPolicyStringBuilder : ObjectPoolPolicy<StringBuilder>
{
    /// <summary>
    /// Gets or sets the initial capacity of pooled <see cref="StringBuilder"/> instances.
    /// </summary>
    /// <value>Defaults to <c>100</c>.</value>
    public int InitialCapacity { get; set; } = 100;

    /// <summary>
    /// Gets or sets the maximum value for <see cref="StringBuilder.Capacity"/> that is allowed to be
    /// retained, when <see cref="Return(StringBuilder)"/> is invoked.
    /// </summary>
    /// <value>Defaults to <c>4096</c>.</value>
    public int MaximumRetainedCapacity { get; set; } = 4 * 1024;

    /// <inheritdoc />
    public override StringBuilder Create()
    {
        return new StringBuilder(InitialCapacity);
    }

    /// <inheritdoc />
    public override bool Return(StringBuilder stringBuilder)
    {
        if (stringBuilder.Capacity > MaximumRetainedCapacity)
        {
            // Too big. Discard this one.
            return false;
        }

        stringBuilder.Clear();

        return true;
    }
}
