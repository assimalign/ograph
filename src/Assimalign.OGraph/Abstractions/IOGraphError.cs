using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

/// <summary>
/// 
/// </summary>
public interface IOGraphError
{
    /// <summary>
    /// 
    /// </summary>
    string Code { get; }
    /// <summary>
    /// 
    /// </summary>
    string Message { get; }
    /// <summary>
    /// 
    /// </summary>
    IOGraphErrorDetailsCollection Details { get; }
}
