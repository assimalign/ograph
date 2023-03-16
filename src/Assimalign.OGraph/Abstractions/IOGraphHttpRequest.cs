using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

public interface IOGraphHttpRequest
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
    IOGraphHttpQueryCollection Query { get; }
    /// <summary>
    /// 
    /// </summary>
    IOGraphHttpHeaderCollection Headers { get; }
    /// <summary>
    /// 
    /// </summary>
    Stream Body { get; }
}
