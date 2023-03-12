using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

public interface IOGraphPropertyResult
{
    /// <summary>
    /// 
    /// </summary>
    bool IsSuccess { get; }
    /// <summary>
    /// 
    /// </summary>
    object? Data { get; }
    /// <summary>
    /// 
    /// </summary>
    IOGraphError? Error { get; }
}
