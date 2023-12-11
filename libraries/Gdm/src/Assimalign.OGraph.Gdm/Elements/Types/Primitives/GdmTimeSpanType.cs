using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;

namespace Assimalign.OGraph.Gdm;

public sealed class GdmTimeSpanType : GdmPrimitiveType<TimeSpan>
{
    public override TimeSpan Read(ref Utf8JsonReader reader)
    {
        throw new NotImplementedException();
    }

    public override TimeSpan Read(XmlReader reader)
    {
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, TimeSpan value)
    {
        throw new NotImplementedException();
    }

    public override void Write(XmlWriter writer, TimeSpan value)
    {
        throw new NotImplementedException();
    }
}
