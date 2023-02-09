using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

/// <summary>
/// 
/// </summary>
public interface IOGraph
{
    /// <summary>
    /// A collection of queries added to the OGraph Model.
    /// </summary>
    IOGraphQueryCollection Queries { get; }
    /// <summary>
    /// A collection of commands added to the OGraph Model.
    /// </summary>
    IOGraphCommandCollection Commands { get;  }
    /// <summary>
    /// 
    /// </summary>
    IOGraphOperationCollection Operations { get; }
}
