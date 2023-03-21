using System;

namespace Assimalign.OGraph;

/// <summary>
/// Types represent primitive, complex, or collection structure that can be 
/// used to define a property, inputs, and outputs of operations within the graph.
/// </summary>
/// <remarks>
/// An <see cref="IOGraphType"/> represents a
/// </remarks>
public interface IOGraphType
{
    /// <summary>
    /// The name of the type.
    /// </summary>
    Name TypeName { get; }
    /// <summary>
    /// 
    /// </summary>
    OGraphTypeIdentifier TypeIdentifier { get; }
    /// <summary>
    /// The underlying .NET Type.
    /// </summary>
    /// <remarks>
    /// All types must have a RuntimeType, even if it is a custom type.
    /// </remarks>
    Type? RuntimeType { get; }

    bool IsNullable { get; }
}