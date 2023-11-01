using System;

namespace Assimalign.OGraph;

/// <summary>
/// Types represent primitive, complex, collections, or enum structure that can be 
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
    Label Label { get; }
    /// <summary>
    /// The identifier of the type.
    /// </summary>
    TypeKind Kind { get; }
    /// <summary>
    /// The underlying .NET Type.
    /// </summary>
    /// <remarks>
    /// All types must have a RuntimeType, even if it is a custom type.
    /// </remarks>
    Type? RuntimeType { get; }
    /// <summary>
    /// Checks whether the <paramref name="value"/> is assignable to the type.
    /// </summary>
    /// <remarks>
    /// <i>This usually entails checking the value against the underlying runtime type.</i>
    /// </remarks>
    /// <param name="value"></param>
    /// <returns></returns>
    bool IsAssignableTo(IOGraphType type);
}