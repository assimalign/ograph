using System.Collections.Generic;

namespace Assimalign.OGraph.Gdm;

/// <summary>
/// 
/// </summary>
public interface IOGraphGdmPropertyCollection : ICollection<IOGraphGdmProperty>
{
    /// <summary>
    /// An index 
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    IOGraphGdmProperty this[GdmName name] { get; }
}