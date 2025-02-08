using System;
using System.Xml;
using System.Text.Json;

namespace Assimalign.OGraph.Gdm.Elements;

using Internal;

public sealed class GdmUriType : GdmScalarType<Uri>
{
    public GdmUriType(GdmGraph graph) 
    {
        Graph = ThrowHelper.ThrowIfNull(graph, nameof(graph));
    }
    public override GdmGraph Graph { get; internal set; }
    public override GdmPrimitiveType PrimitiveType => GdmPrimitiveType.String;
    public override Uri Read(ref Utf8JsonReader reader)
    {
        if (reader.TokenType != JsonTokenType.String)
        {
            ThrowHelper.ThrowInvalidDeserializationContentException("");
        }

        var value = reader.GetString();

        if (!Uri.TryCreate(value, UriKind.RelativeOrAbsolute, out var uri))
        {
            ThrowHelper.ThrowInvalidDeserializationContentException("", new JsonException());
        }

        return uri;
    }
    public override Uri Read(XmlReader reader)
    {
        var value = reader.ReadContentAsString();

        if (!Uri.TryCreate(value, UriKind.RelativeOrAbsolute, out var uri))
        {
            ThrowHelper.ThrowInvalidDeserializationContentException("", new XmlException(""));
        }

        return uri;
    }
    public override void Write(Utf8JsonWriter writer, Uri value)
    {
        writer.WriteStringValue(value.ToString());
    }
    public override void Write(XmlWriter writer, Uri value)
    {
        writer.WriteValue(value.ToString());
    }
    public override Uri Parse(string? value)
    {
        if (TryParse(value, out var result))
        {
            return result;
        }

        throw new ArgumentException("");
    }
    public override bool TryParse(string? value, out Uri result)
    {
        return Uri.TryCreate(value, UriKind.RelativeOrAbsolute, out result!);
    }
}
