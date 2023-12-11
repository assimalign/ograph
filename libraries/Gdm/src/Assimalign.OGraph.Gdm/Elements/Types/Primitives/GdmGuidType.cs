using System;
using System.Text.Json;
using System.Xml;

namespace Assimalign.OGraph.Gdm;

public sealed class GdmGuidType : GdmPrimitiveType<Guid>
{
    public override Guid Read(ref Utf8JsonReader reader)
    {
        throw new NotImplementedException();
    }

    public override Guid Read(XmlReader reader)
    {
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, Guid value)
    {
        throw new NotImplementedException();
    }

    public override void Write(XmlWriter writer, Guid value)
    {
        throw new NotImplementedException();
    }
}
