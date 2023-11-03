using System.Collections.Generic;

namespace Assimalign.OGraph.Gdm;

/// <summary>
/// 
/// </summary>
public interface IOGraphGdmPropertyCollection : IList<IOGraphGdmProperty>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    IOGraphGdmProperty this[Label name] { get; }
    ///// <summary>
    ///// 
    ///// </summary>
    ///// <param name="property"></param>
    ///// <returns></returns>
    //bool TryAddProperty(IOGraphGdmProperty property);
    ///// <summary>
    ///// 
    ///// </summary>
    ///// <param name="name"></param>
    ///// <param name="property"></param>
    ///// <returns></returns>
    //bool TryGetProperty(Label name, out IOGraphGdmProperty? property);
}
