using System;
using System.Collections.Generic;

namespace Assimalign.OGraph;

/// <summary>
/// 
/// </summary>
public interface IOGraphComplexType : IOGraphType
{
    /// <summary>
    /// A collection of properties
    /// </summary>
    IOGraphPropertyCollection Properties { get; }


}
