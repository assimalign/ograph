using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

/// <summary>
/// Represents a collection of metadata 
/// </summary>
public interface IOGraphNodePropertyMetadata : IEnumerable<KeyValuePair<string, object>>
{
}
