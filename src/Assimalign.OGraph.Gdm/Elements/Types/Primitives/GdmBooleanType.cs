using System.Xml;
using System.Text.Json;

namespace Assimalign.OGraph.Gdm;

using Assimalign.OGraph.Gdm.Internal;

public sealed class GdmBooleanType : GdmPrimitiveType<bool>
{
    public override bool Read(ref Utf8JsonReader reader)
    {
        if (!reader.IsBooleanToken())
        {
         
          
        }
        return reader.GetBoolean();
    }
    public override bool Read(XmlReader reader)
    {
        return reader.ReadElementContentAsBoolean();
    }
    public override void Write(Utf8JsonWriter writer, bool value)
    {
        writer.WriteBooleanValue(value);
    }
    public override void Write(XmlWriter writer, bool value)
    {
        writer.WriteValue(value);
    }
}
