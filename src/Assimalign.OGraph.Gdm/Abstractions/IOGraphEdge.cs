using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Modeling;

/// <summary>
/// 
/// </summary>
/// <remarks>
/// An Edge is also referred to as a Link.
/// </remarks>
public interface IOGraphEdge
{
    /// <summary>
    /// 
    /// </summary>
    IOGraphNode Node { get; }
}
