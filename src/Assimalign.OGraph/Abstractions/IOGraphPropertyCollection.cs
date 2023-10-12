using System.Collections.Generic;

namespace Assimalign.OGraph;

public interface IOGraphPropertyCollection : ICollection<IOGraphProperty>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <param name="property"></param>
    /// <returns></returns>
    bool TryGetProperty(Name name, out IOGraphProperty? property);
}
