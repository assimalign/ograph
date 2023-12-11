using System.Xml;
using System.Text.Json;

namespace Assimalign.OGraph.Gdm;

public sealed class GdmByteType : GdmPrimitiveType<byte>
{
    public override byte Read(ref Utf8JsonReader reader)
    {
        throw new System.NotImplementedException();
    }

    public override byte Read(XmlReader reader)
    {
        throw new System.NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, byte value)
    {
        throw new System.NotImplementedException();
    }

    public override void Write(XmlWriter writer, byte value)
    {
        throw new System.NotImplementedException();
    }
}