using System;
using System.Collections.Generic;

namespace Assimalign.OGraph;

public interface IOGraphEntity
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
