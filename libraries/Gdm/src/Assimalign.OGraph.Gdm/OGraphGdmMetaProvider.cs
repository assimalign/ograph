using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm;

public abstract class OGraphGdmMetaProvider
{
    /// <summary>
    /// The name of the provider.
    /// </summary>
    public abstract string Name { get; }


    Task SerializeAsync()
}
