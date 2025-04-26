using System.Collections;
using System.Collections.Generic;

namespace Assimalign.OGraph.Gdm;

/// <summary>
/// 
/// </summary>
public interface IOGraphGdmComplexType : IOGraphGdmType
{
    /// <summary>
    /// 
    /// </summary>
    IOGraphGdmMemberCollection Members { get; }
}