using System;
using System.Xml;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

public interface IOGraphEdgeResult
{
    /// <summary>
    /// 
    /// </summary>
    IOGraphError? Error { get; }
}
