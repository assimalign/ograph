using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Assimalign.OGraph.Gdm;

namespace Assimalign.OGraph.Client;

/// <summary>
/// A named client factory.
/// </summary>
public interface IOGraphClientFactory
{
    /// <summary>
    /// Create a client with a given name.
    /// </summary>
    /// <param name="label"></param>
    /// <returns></returns>
    IOGraphClient Create(GdmLabel label);
}