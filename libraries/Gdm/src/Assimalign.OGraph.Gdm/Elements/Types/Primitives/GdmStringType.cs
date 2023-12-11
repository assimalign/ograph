using System;
using System.Text.Json;
using System.Xml;

namespace Assimalign.OGraph.Gdm;

public sealed class GdmStringType : GdmPrimitiveType<string>
{
    public override string Read(ref Utf8JsonReader reader)
    {
        throw new NotImplementedException();
    }

    public override string Read(XmlReader reader)
    {
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, string value)
    {
        throw new NotImplementedException();
    }

    public override void Write(XmlWriter writer, string value)
    {
        throw new NotImplementedException();
    }
}
