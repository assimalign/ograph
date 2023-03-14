using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;

namespace Assimalign.OGraph;

public sealed class QueryableType<T> : CollectionType<IQueryable<T>>
    where T : class
{



    public IQueryable<T> Queryable { get; init; }

    public override IOGraphType ItemType => throw new NotImplementedException();

    public override bool TryReadJson(Utf8JsonReader reader, out OGraphCollection collection)
    {
        throw new NotImplementedException();
    }

    public override bool TryReadXml(XmlReader reader, out OGraphCollection collection)
    {
        throw new NotImplementedException();
    }

    public override bool TryWriteJson(Utf8JsonWriter writer, OGraphCollection collection)
    {
        throw new NotImplementedException();
    }

    public override bool TryWriteXml(XmlWriter writer, OGraphCollection collection)
    {
        throw new NotImplementedException();
    }
}
