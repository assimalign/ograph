using System;
using System.Xml;
using System.Text.Json;

namespace Assimalign.OGraph.Gdm;

using Internal;

public sealed class GdmInt16Type : GdmPrimitiveType<Int16>
{
    public override short Read(ref Utf8JsonReader reader)
    {
        return reader.GetInt16();
    }

    public override short Read(XmlReader reader)
    {
        var nodeType = reader.NodeType;

        // Check for content. Should have read the element.
        if (nodeType != XmlNodeType.Text)
        {
            GdmThrowHelper.ThrowInvalidContentException("");
        }

        var content = reader.ReadContentAsString();

        if (!short.TryParse(content, out var value))
        {

        }

        return value;
    }

    public override void Write(Utf8JsonWriter writer, short value)
    {
        writer.WriteNumberValue(value);
    }

    public override void Write(XmlWriter writer, short value)
    {
        writer.WriteValue(value);
    }
}
