using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Execution;

public interface IOGraphRequest
{
    /// <summary>
    /// 
    /// </summary>
    Route Route { get; }
    /// <summary>
    /// 
    /// </summary>
    Method Method { get; }
    /// <summary>
    /// 
    /// </summary>
    IOGraphQueryCollection Query { get; }
    /// <summary>
    /// 
    /// </summary>
    IOGraphHeaderCollection Headers { get; }
    /// <summary>
    /// 
    /// </summary>
    Stream Body { get; }
}
