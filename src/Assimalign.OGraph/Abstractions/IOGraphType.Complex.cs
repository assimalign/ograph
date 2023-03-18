using System;
using System.Collections.Generic;

namespace Assimalign.OGraph;

/// <summary>
/// 
/// </summary>
public interface IOGraphComplexType : IOGraphType
{
    /// <summary>
    /// 
    /// </summary>
    IOGraphPropertyCollection Properties { get; }
}
