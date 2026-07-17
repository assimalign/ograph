using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Assimalign.OGraph.Gdm;

using Elements;
using Assimalign.OGraph.Gdm.Internal;

public static class OGraphGdmTypeExtensions
{
    public static GdmProperty CreateProperty<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] TGdmType>(this GdmEntityType type, GdmName name)
        where TGdmType : GdmType
    {
        ThrowHelper.ThrowIfNull(type);

        GdmType? instance = default;

        switch ((instance = Activator.CreateInstance(typeof(TGdmType), type.Graph) as GdmType))
        {
            case GdmCollectionType collection when collection.ItemType is GdmEntityType:
                throw new InvalidOperationException("");
            case GdmEntityType entity:
                throw new InvalidOperationException("");
            case null:
                throw new InvalidOperationException();
        }

        var existing = (type.Graph.Types as IEnumerable<GdmType>).FirstOrDefault(p => p.Name == instance.Name);

        if (existing is null)
        {
            type.Graph.Types.Add(instance);

            return new GdmProperty(name, instance, type);
        }
        else
        {
            return new GdmProperty(name, existing, type);
        }
    }

    ///// <summary>
    ///// 
    ///// </summary>
    ///// <typeparam name="T"></typeparam>
    ///// <param name="gdmType"></param>
    ///// <returns></returns>
    //public static IOGraphGdmCollectionType AsCollection<
    //    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties)] T
    //    >(this GdmType<T> gdmType)
    //{
    //    if (gdmType is null)
    //    {
    //        ThrowHelper.ThrowArgumentNullException(nameof(gdmType));
    //    }
    //    return new GdmListType<T>(gdmType);
    //}

    //public static bool TryGetProperty(this IOGraphGdmComplexType complexType, GdmLabel label, out IOGraphGdmProperty? property)
    //{
    //    property = default;

    //    foreach (var item in complexType.Members.OfType<IOGraphGdmProperty>())
    //    {
    //        if (item.Label == label)
    //        {
    //            property = item;
    //            return true;
    //        }
    //    }

    //    return false;
    //}
    //public static IOGraphGdmPropertyDescriptor<T>
}
