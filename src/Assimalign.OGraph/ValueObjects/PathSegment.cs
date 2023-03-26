using System;

namespace Assimalign.OGraph;

public readonly struct PathSegment
{

    internal PathSegment(string value)
    {
        if (value is null)
        {
            throw new ArgumentNullException(nameof(value));
        }
        this.Value = value;
    }

    /// <summary>
    /// The raw segment value.
    /// </summary>
    public string Value { get; }
}
