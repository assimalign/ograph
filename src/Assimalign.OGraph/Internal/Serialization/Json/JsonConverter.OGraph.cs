using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Internal;

internal class OGraphJsonConverter : JsonConverter<IOGraph>
{
    public override IOGraph? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, IOGraph value, JsonSerializerOptions options)
    {
        if (value is null)
        {
            
        }
        writer.WriteStartObject();
        




        writer.WriteEndObject();
    }
}
