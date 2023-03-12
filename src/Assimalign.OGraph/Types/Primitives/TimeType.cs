using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;

namespace Assimalign.OGraph;

public sealed class TimeType : ValueType<TimeOnly>
{
    public override bool TryReadJson(Utf8JsonReader reader, out OGraphValue value)
    {
        value = default;

        if (reader.TokenType != JsonTokenType.String)
        {
            return false;
        }

        var timeString = reader.GetString();

        if (TimeOnly.TryParse(timeString, out var timeOnly))
        {
            value = new(timeOnly);
            return true;
        }

        return false;
    }
    public override bool TryReadXml(XmlReader reader, out OGraphValue value)
    {
        value = default;

        if (reader.NodeType != XmlNodeType.Text)
        {
            return false;
        }

        var timeString = reader.ReadElementContentAsString();

        if (TimeOnly.TryParse(timeString, out var timeOnly))
        {
            value = new(timeOnly);
            return true;
        }

        throw new NotImplementedException();
    }
    public override bool TryWriteJson(Utf8JsonWriter writer, OGraphValue value)
    {
        if (value.Value is not TimeOnly timeOnly)
        {
            return false;
        }

        writer.WriteStringValue(timeOnly.ToString());

        return true;
    }
    public override bool TryWriteXml(XmlWriter writer, OGraphValue value)
    {
        throw new NotImplementedException();
    }
}
