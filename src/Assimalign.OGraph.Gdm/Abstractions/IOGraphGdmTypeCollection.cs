using System;
using System.Collections.Generic;

namespace Assimalign.OGraph.Gdm;

/// <summary>
/// 
/// </summary>
public interface IOGraphGdmTypeCollection : ICollection<IOGraphGdmType>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    IOGraphGdmType this[Label name] { get; }
}