using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;

namespace Assimalign.OGraph;

public sealed class UIntType : ValueType<uint>
{
    public override bool TryReadJson(Utf8JsonReader reader, out OGraphValue value)
    {
        throw new NotImplementedException();
    }

    public override bool TryReadXml(XmlReader reader, out OGraphValue value)
    {
        throw new NotImplementedException();
    }

    public override bool TryWriteJson(Utf8JsonWriter writer, OGraphValue value)
    {
        throw new NotImplementedException();
    }

    public override bool TryWriteXml(XmlWriter writer, OGraphValue value)
    {
        throw new NotImplementedException();
    }
}
