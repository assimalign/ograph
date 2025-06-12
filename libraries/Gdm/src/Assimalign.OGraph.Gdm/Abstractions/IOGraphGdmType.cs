using System;
using System.Xml;
using System.Text.Json;
using System.Diagnostics.CodeAnalysis;

namespace Assimalign.OGraph.Gdm;

/// <summary>
/// Types represent primitive, complex, collections, or enum structure that can be 
/// used to define a property, inputs, and outputs of operations within the graph.
/// </summary>
/// <remarks>
/// An <see cref="IOGraphGdmType"/> represents a
/// </remarks>
public interface IOGraphGdmType : IOGraphGdmNamedElement
{
    /// <summary>
    /// The underlying .NET Type. All types must have a RuntimeType, even if it is a custom type.
    /// </summary>
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
    Type RuntimeType { get; }

    /// <summary>
    /// The identifier of the type.
    /// </summary>
    GdmTypeKind Kind { get; }

    /// <summary>
    /// The Graph in which the type belongs to.
    /// </summary>
    IOGraphGdmGraph Graph { get; }

    /// <summary>
    /// Writes the provided <paramref name="value"/> to JSON.
    /// </summary>
    /// <param name="writer"></param>
    /// <param name="value"></param>
    void Write(Utf8JsonWriter writer, object value);

    /// <summary>
    /// Writes the provided <paramref name="value"/> to XML.
    /// </summary>
    /// <param name="writer"></param>
    /// <param name="value"></param>
    void Write(XmlWriter writer, object value);

    /// <summary>
    /// Reads an object from JSON.
    /// </summary>
    /// <param name="reader"></param>
    /// <returns></returns>
    object Read(ref Utf8JsonReader reader);

    /// <summary>
    /// Reads an object from XML.
    /// </summary>
    /// <param name="reader"></param>
    /// <returns></returns>
    object Read(XmlReader reader);
}