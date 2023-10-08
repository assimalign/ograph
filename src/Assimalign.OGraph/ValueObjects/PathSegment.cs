using System;
using System.Diagnostics;

namespace Assimalign.OGraph;

[DebuggerDisplay("Path Segment: {Value}")]
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