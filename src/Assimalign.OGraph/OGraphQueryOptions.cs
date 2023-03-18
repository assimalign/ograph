using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

/// <summary>
/// 
/// </summary>
public abstract class OGraphQueryOptions
{
    /// <summary>
    /// Enables or disables sorting. Default is true;
    /// </summary>
    public bool CanSort { get; set; } = true;
    /// <summary>
    /// Enables or disables filtering. Default is true.
    /// </summary>
    public bool CanFilter { get; set; } = true;
    /// <summary>
    /// Enables or disables paging. Default is true.
    /// </summary>
    public bool CanPage { get; set; } = true;
    /// <summary>
    /// Enables or disables projections. Default is true.
    /// </summary>
    public bool CanProject { get; set; } = true;
}
