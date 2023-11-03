using System;
using System.Collections.Generic;

namespace Assimalign.OGraph.Gdm;

public static class OGraphGdmPropertyExtensions
{
    public static IEnumerable<IOGraphGdmInputBinding> GetInputBindings(this IOGraphGdmProperty property)
    {
        foreach (var binding in property.GetBindings())
        {
            if (binding is IOGraphGdmInputBinding input)
            {
                yield return input;
            }
        }
    }

    public static IEnumerable<IOGraphGdmOutputBinding> GetOutputBindings(this IOGraphGdmProperty property)
    {
        foreach (var binding in property.GetBindings())
        {
            if (binding is IOGraphGdmOutputBinding output)
            {
                yield return output;
            }
        }
    }
}
