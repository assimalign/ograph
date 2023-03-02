using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

public interface IOGraphMetadata : IEnumerable<KeyValuePair<string, object>>
{

    object this[string key] { get; set; }
}
