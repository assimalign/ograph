using System;
using System.Collections.Generic;

namespace Assimalign.OGraph.Gdm;

/// <summary>
/// 
/// </summary>
public interface IOGraphGdmComplexType : IOGraphGdmType
{
    /// <summary>
    /// A collection of properties
    /// </summary>
    IOGraphGdmPropertyCollection Properties { get; }
}