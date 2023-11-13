using System.Collections.Generic;

namespace Assimalign.OGraph;

public interface IOGraphPropertyCollection : ICollection<IOGraphProperty>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    IOGraphProperty this[Label name] { get; }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="property"></param>
    /// <returns></returns>
    bool TryAddProperty(IOGraphProperty property);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <param name="property"></param>
    /// <returns></returns>
    bool TryGetProperty(Label name, out IOGraphProperty? property);
}
