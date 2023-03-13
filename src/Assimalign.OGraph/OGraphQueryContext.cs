using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

public sealed class OGraphQueryContext
{
    /// <summary>
    /// 
    /// </summary>
    public IOGraph Graph { get; init; }
    /// <summary>
    /// 
    /// </summary>
    public IOGraphNode Node { get; init; }
    /// <summary>
    /// 
    /// </summary>
    public OGraphQueryOptions Options { get; init; }
}