using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Assimalign.OGraph.Gdm;

using Assimalign.OGraph.Gdm.Elements;
using Assimalign.OGraph.Gdm.Internal;

public static class OGraphGdmTypeExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="gdmType"></param>
    /// <returns></returns>
    public static IOGraphGdmCollectionType AsCollection<
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties)] T
        >(this GdmType<T> gdmType)
    {
        if (gdmType is null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(gdmType));
        }
        return new GdmListType<T>(gdmType);
    }

    public static bool TryGetProperty(this IOGraphGdmComplexType complexType, GdmLabel label, out IOGraphGdmProperty? property)
    {
        property = default;

        foreach (var item in complexType.Members.OfType<IOGraphGdmProperty>())
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
