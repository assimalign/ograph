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
    /// <param name="response"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task ExecuteAsync(IOGraphExecutorContext context, CancellationToken cancellationToken = default);
}