using Assimalign.OGraph.Gdm.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;

namespace Assimalign.OGraph.Gdm;

public sealed class GdmUriType : GdmPrimitiveType<Uri>
{
    public override Uri? Read(ref Utf8JsonReader reader)
    {
        if (reader.IsStringToken())
        {
            return new Uri(reader.GetString()!);
        }
        if (reader.IsNullToken())
        {
            return null;
        }
        return base.Read(ref reader);
    }

    public override Uri? Read(XmlReader reader)
    {
        return base.Read(reader);
    }

    public override void Write(Utf8JsonWriter writer, Uri? value)
    {
        base.Write(writer, value);
    }

    public override void Write(XmlWriter writer, Uri? value)
    {
        base.Write(writer, value);
    }
}
