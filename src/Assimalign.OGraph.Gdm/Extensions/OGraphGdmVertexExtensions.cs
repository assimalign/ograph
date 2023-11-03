using System;
using System.Linq;
using System.Collections.Generic;

namespace Assimalign.OGraph.Gdm;

public static class OGraphGdmVertexExtensions
{

    public static IEnumerable<IOGraphGdmInputBinding> GetInputBindings(this IOGraphGdmVertex vertex)
    {
        foreach (var binding in vertex.GetBindings())
        {
            if (binding is IOGraphGdmInputBinding input)
            {
                yield return input;
            }
        }
    }

    public static IEnumerable<IOGraphGdmOutputBinding> GetOutputBindings(this IOGraphGdmVertex vertex)
    {
        foreach (var binding in vertex.GetBindings())
        {
            if (binding is IOGraphGdmOutputBinding output)
            {
                yield return output;
            }
        }
    }
}
