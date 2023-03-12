using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

/// <summary>
/// 
/// </summary>
public interface IOGraphOperationResult
{
    /// <summary>
    /// The status code of the OGraph operation to
    /// </summary>
    int StatusCode { get; }
    /// <summary>
    /// 
    /// </summary>
    bool IsSuccess { get; }
    /// <summary>
    /// 
    /// </summary>
    object Data { get; }
    /// <summary>
    /// 
    /// </summary>
    IOGraphError? Error { get; }
    
}

