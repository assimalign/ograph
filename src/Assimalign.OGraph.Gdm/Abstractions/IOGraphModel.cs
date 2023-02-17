using System;
using System.Collections.Generic;

namespace Assimalign.OGraph.Modeling;

public interface IOGraphModel
{
    /// <summary>
    /// The name of the Entity.
    /// </summary>
    string Name { get; }
    /// <summary>
    /// 
    /// </summary>
    Type Type { get; }
    /// <summary>
    /// 
    /// </summary>
    IEnumerable<IOGraphEntityMember> Members { get; }
}
