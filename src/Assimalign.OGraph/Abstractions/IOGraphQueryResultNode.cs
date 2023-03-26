using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

public interface IOGraphQueryResultNode : IEnumerable<KeyValuePair<string, object>>
{
    /// <summary>
    /// 
    /// </summary>
    IOGraphQueryResultEdgeCollection Edges { get; }
}