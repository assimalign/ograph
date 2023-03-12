using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Execution;

public interface IOGraphHeaderCollection : IDictionary<string, HeaderValue>
{
    /// <summary>
    /// 
    /// </summary>
    HeaderValue Accept { get; }
    /// <summary>
    /// 
    /// </summary>
    HeaderValue ContentType { get; }
}
