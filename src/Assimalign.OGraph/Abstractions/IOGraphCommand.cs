using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

public interface IOGraphCommand : IOGraphOperation
{
   
    /// <summary>
    /// 
    /// </summary>
    IOGraphCommandType CommandType { get; }
    /// <summary>
    /// 
    /// </summary>
    IOGraphResolver Resolver { get; }
}
