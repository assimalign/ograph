using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

/// <summary>
/// 
/// </summary>
public interface IOGraphSchema
{
    /// <summary>
    /// The name of the Graph Schema.
    /// </summary>
    string Name { get;  }
    /// <summary>
    /// 
    /// </summary>
    IEnumerable<IOGraphEntity> Entities { get; }
}
