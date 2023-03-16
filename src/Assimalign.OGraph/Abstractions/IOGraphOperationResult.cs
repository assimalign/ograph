using System;
using System.Xml;
using System.Text.Json;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

/// <summary>
/// 
/// </summary>
public interface IOGraphOperationResult
{
    /// <summary>
    /// 
    /// </summary>
    IOGraphError? Error { get; }
}