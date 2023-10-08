using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml;

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
    /// The identifier
    /// </summary>
    OGraphTypeIdentifier TypeIdentifier { get; }
    /// <summary>
    /// The underlying .NET Type.
    /// </summary>
    /// <remarks>
    /// All types must have a RuntimeType, even if it is a custom type.
    /// </remarks>
    Type? RuntimeType { get; }
    /// <summary>
    /// 
    /// </summary>
    bool IsNullable { get; }


    // TODO: Need to revisit Serialization strategy. Not sure if this is the best option
    [Obsolete("Not ready for general consumption.")]
    void Write(XmlWriter writer, object value);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="writer"></param>
    /// <param name="value"></param>
    [Obsolete("Not ready for general consumption.")]
    void Write(Utf8JsonWriter writer, object value);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="reader"></param>
    /// <returns></returns>
    [Obsolete("Not ready for general consumption.")]
    object Read(XmlReader reader);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="reader"></param>
    /// <returns></returns>
    [Obsolete("Not ready for general consumption.")]
    object Read(ref Utf8JsonReader reader);
}