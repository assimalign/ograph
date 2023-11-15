using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm.Internal;

internal static class JsonReaderExtensions
{
    internal static bool IsBooleanToken(this ref Utf8JsonReader reader) => 
        reader.TokenType == JsonTokenType.True || reader.TokenType == JsonTokenType.False;

    internal static bool IsStringToken(this ref Utf8JsonReader reader) =>
        reader.TokenType == JsonTokenType.String;

    internal static bool IsNullToken(this ref Utf8JsonReader reader) =>
        reader.TokenType == JsonTokenType.Null;

    internal static bool IsStartOfObjectToken(this ref Utf8JsonReader reader) =>
        reader.TokenType == JsonTokenType.StartObject;

    internal static bool IsEndOfObjectToken(this ref Utf8JsonReader reader) => 
        reader.TokenType == JsonTokenType.EndObject;

    internal static bool IsStartOfArrayToken(this ref Utf8JsonReader reader) =>
        reader.TokenType == JsonTokenType.StartObject;

    internal static bool IsEndOfArrayToken(this ref Utf8JsonReader reader) =>
        reader.TokenType == JsonTokenType.EndObject;
}
