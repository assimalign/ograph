using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;

namespace Assimalign.OGraph.Gdm;

public sealed class GdmNullCharType : GdmPrimitiveType<char?>
{
    public override char? Read(ref Utf8JsonReader reader)
    {
        var value = reader.GetString();

        if (value.Length > 1)
        {
            throw new JsonException("");
        }

        return value[0];
    }

    public override char? Read(XmlReader reader)
    {
        //return reader.ReadElementContentAsBoolean();
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, char? value)
    {
        throw new NotImplementedException();
    }

    public override void Write(XmlWriter writer, char? value)
    {
        throw new NotImplementedException();
    }
}