using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Internal;

internal class QueryResultJsonConverter : JsonConverter<IOGraphQueryResult>
{
    public override IOGraphQueryResult? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, IOGraphQueryResult value, JsonSerializerOptions options)
    {
        var errorConverter = options.GetConverter(typeof(IOGraphError)) as JsonConverter<IOGraphError>;
        var pageConverter = options.GetConverter(typeof(IOGraphQueryResultPageInfo)) as JsonConverter<IOGraphQueryResultPageInfo>;
        var nodeConverter = options.GetConverter(typeof(IOGraphQueryResultNodeCollection)) as JsonConverter<IOGraphQueryResultNodeCollection>;

        writer.WriteStartObject();

        writer.WritePropertyName(nameof(IOGraphQueryResult.PageInfo));
        pageConverter.Write(writer, value.PageInfo, options);

        writer.WritePropertyName(nameof(IOGraphQueryResult.Error));
        errorConverter.Write(writer, value.Error, options);

        writer.WritePropertyName(nameof(IOGraphQueryResult.Nodes));
        nodeConverter.Write(writer, value.Nodes, options);

        writer.WriteEndObject();
    }
}
