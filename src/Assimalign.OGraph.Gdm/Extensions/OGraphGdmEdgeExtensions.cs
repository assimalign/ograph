using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm;

public static class OGraphGdmEdgeExtensions
{
    public static IEnumerable<IOGraphGdmInputBinding> GetInputBindings(this IOGraphGdmEdge edge)
    {
        foreach (var binding in edge.GetBindings())
        {
            if (binding is IOGraphGdmInputBinding input)
            {
                yield return input;
            }
        }
    }

    public static IEnumerable<IOGraphGdmOutputBinding> GetOutputBindings(this IOGraphGdmEdge edge)
    {
        foreach (var binding in edge.GetBindings())
        {
            if (binding is IOGraphGdmOutputBinding output)
            {
                yield return output;
            }
        }
    }
}
