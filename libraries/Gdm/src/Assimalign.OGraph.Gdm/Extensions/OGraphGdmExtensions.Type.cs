using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

namespace Assimalign.OGraph.Gdm;

using Assimalign.OGraph.Gdm.Internal;

public static class OGraphGdmTypeExtensions
{

    public static bool TryGetProperty(this IOGraphGdmComplexType complexType, Label label, out IOGraphGdmProperty? property)
    {
        property = default;

        foreach (var item in complexType.Properties)
        {
            if (item.Label == label)
            {
                property = item;
                return true;
            }
        }

        return false;
    }
    //public static IOGraphGdmPropertyDescriptor<T>
}
