using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Xml;

namespace Assimalign.OGraph.Gdm;

/// <summary>
/// 
/// </summary>
public interface IOGraphGdmSerializableType : IOGraphGdmType
{
    /// <summary>
    /// The underlying .NET Type. All types must have a RuntimeType, even if it is a custom type.
    /// </summary>
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
    Type RuntimeType { get; }

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
