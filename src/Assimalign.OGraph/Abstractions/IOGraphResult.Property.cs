using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

/// <summary>
/// 
/// </summary>
public interface IOGraphPropertyResult : IOGraphResult
{
    /// <summary>
    /// 
    /// </summary>
    object Value { get; }
}
