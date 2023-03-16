using System;
using System.Collections.Generic;

namespace Assimalign.OGraph;

public interface IOGraphMetadata : IEnumerable<KeyValuePair<string, object>>
{

    object this[string key] { get; set; }
}
