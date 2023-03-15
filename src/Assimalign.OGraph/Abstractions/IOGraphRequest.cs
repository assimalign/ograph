using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

public interface IOGraphRequest
{
    /// <summary>
    /// 
    /// </summary>
    Method Method { get; }
    /// <summary>
    /// The request path
    /// </summary>
    Path Path { get; }
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
