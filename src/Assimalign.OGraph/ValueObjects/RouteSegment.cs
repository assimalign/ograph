using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

public readonly struct RouteSegment
{

    public bool IsParameter { get; }

    /// <summary>
    /// Specifies whether the route segment is a literal.
    /// </summary>
    public bool IsLiteral { get; }
}
