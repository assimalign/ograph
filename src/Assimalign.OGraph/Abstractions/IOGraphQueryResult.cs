using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

public interface IOGraphQueryResult
{
    /// <summary>
    /// 
    /// </summary>
    long TotalCount { get; }
    /// <summary>
    /// 
    /// </summary>
    IOGraphQueryPageInfo PageInfo { get; }
    /// <summary>
    /// 
    /// </summary>
    object Data { get; }
}