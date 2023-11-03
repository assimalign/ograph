using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

using Assimalign.OGraph.Gdm;

public interface IOGraphPropertyResolverContext : IOGraphGdmPropertyBindingContext
{
    /// <summary>
    /// Returns the most recent resolved parent object.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    T GetParent<T>();
}
