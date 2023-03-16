using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

public interface IOGraphPropertyResult
{
    /// <summary>
    /// 
    /// </summary>
    IOGraphError? Error { get; }
    /// <summary>
    /// 
    /// </summary>
    object? Value { get; }
}
