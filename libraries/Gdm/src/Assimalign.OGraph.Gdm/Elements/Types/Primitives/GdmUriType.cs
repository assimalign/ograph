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
    public override Uri Read(ref Utf8JsonReader reader)
    {
        return new Uri(reader.GetString()!);
    }

    public override Uri Read(XmlReader reader)
    {
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, Uri value)
    {
        throw new NotImplementedException();
    }

    public override void Write(XmlWriter writer, Uri value)
    {
        throw new NotImplementedException();
    }
}
