using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

public sealed class EntityChange
{
    /// <summary>
    /// The name of the property that changes.
    /// </summary>
    public string? PropertyName { get; set; }
    /// <summary>
    /// Specifies the type of change that occurred on the property.
    /// </summary>
    public EntityChangeType ChangeType { get; set; }
    /// <summary>
    /// Represents the current state of the
    /// </summary>
    public object? Current { get; set; }
    /// <summary>
    /// Represents the original value before state change.
    /// </summary>
    public object? Original { get; set; }
    /// <summary>
    /// Specifies that the property change went from changing to changed.
    /// </summary>
    internal bool HasChanged { get; set; }
}
