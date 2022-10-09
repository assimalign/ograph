using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    IEnumerable<IOGraphEntityMember> Members { get; }


}
