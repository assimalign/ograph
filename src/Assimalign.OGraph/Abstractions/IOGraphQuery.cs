using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

public interface IOGraphQuery : IOGraphOperation
{
    /// <summary>
    /// 
    /// </summary>
    IOGraphQueryType QueryType { get; }
    /// <summary>
    /// 
    /// </summary>
    IOGraphResolver Resolver { get;  }
}
