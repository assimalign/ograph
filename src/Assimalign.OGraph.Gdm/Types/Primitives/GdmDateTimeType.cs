using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;

namespace Assimalign.OGraph.Gdm;

public sealed class GdmDateTimeType : GdmPrimitiveType<DateTime>
{
    public override DateTime Read(ref Utf8JsonReader reader)
    {
        return base.Read(ref reader);
    }

    public override DateTime Read(XmlReader reader)
    {
        return base.Read(reader);
    }

    public override void Write(Utf8JsonWriter writer, DateTime value)
    {
        base.Write(writer, value);
    }

    public override void Write(XmlWriter writer, DateTime value)
    {
        base.Write(writer, value);
    }
}
