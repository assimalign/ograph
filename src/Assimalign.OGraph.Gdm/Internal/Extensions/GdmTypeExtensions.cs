using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;


namespace Assimalign.OGraph.Gdm.Internal;

internal static class GdmTypeExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public static IOGraphGdmType GetGdmType(this Type type)
    {
        if (type.TryGetGdmType(out var gdmType))
        {
            return gdmType!;
        }

        throw new Exception();
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    /// <param name="gdmType"></param>
    /// <returns></returns>
    public static bool TryGetGdmType(this Type type, out IOGraphGdmType? gdmType)
    {
        gdmType = null;

        if (type.TryGetGdmPrimitiveType(out var gdmPrimitiveType))
        {
            gdmType = gdmPrimitiveType;
            return true;
        }
        if (type.TryGetGdmEnumType(out var gdmEnumType))
        {
            gdmType = gdmEnumType;
            return true;
        }
        if (type.TryGetGdmCollectionType(out var gdmCollectionType))
        {
            gdmType = gdmCollectionType;
            return true;
        }
        if (type.TryGetGdmComplexType(out var gdmComplexType))
        {
            gdmType = gdmComplexType;
            return true;
        }


        return false;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public static IOGraphGdmEnumType GetGdmEnumType(this Type type)
    {
        if (type.TryGetGdmEnumType(out var gdmEnumType))
        {
            return gdmEnumType!;
        }
        throw new Exception();
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    /// <param name="gdmCollectionType"></param>
    /// <returns></returns>
    public static bool TryGetGdmEnumType(this Type type, out IOGraphGdmEnumType? gdmEnumType)
    {
        gdmEnumType = null;

        if (type.IsEnum)
        {
            var enumType = typeof(GdmEnumType<>).MakeGenericType(type);
            gdmEnumType = (Activator.CreateInstance(enumType) as IOGraphGdmEnumType)!;
            return true;
        }
        var typeArgs = type.GetGenericArguments();
        if (typeArgs.Length == 1 &&
            typeArgs[0].IsEnum &&
            typeof(Nullable<>).MakeGenericType(typeArgs[0]).IsAssignableTo(type))
        {
            var enumType = typeof(GdmNullEnumType<>).MakeGenericType(typeArgs[0]);
            gdmEnumType = (Activator.CreateInstance(enumType) as IOGraphGdmEnumType)!;
            return true;
        }

        return false;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public static IOGraphGdmCollectionType GetGdmCollectionType(this Type type)
    {
        if (type.TryGetGdmCollectionType(out var gdmCollectionType))
        {
            return gdmCollectionType!;
        }
        throw new Exception();
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    /// <param name="gdmCollectionType"></param>
    /// <returns></returns>
    public static bool TryGetGdmCollectionType(this Type type, out IOGraphGdmCollectionType? gdmCollectionType)
    {
        gdmCollectionType = null;

        if (type.IsEnumerableType(out var enumerableType))
        {
            if (enumerableType.TryGetGdmType(out var gdmType))
            {
                var collectionType = typeof(GdmCollectionType<>).MakeGenericType(gdmType!.GetType());
                gdmCollectionType = (Activator.CreateInstance(collectionType) as IOGraphGdmCollectionType)!;
                return true;
            }
        }

        return false;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static IOGraphGdmComplexType GetGdmComplexType(this Type type)
    {
        if (type.TryGetGdmComplexType(out var gdmComplexType))
        {
            return gdmComplexType!;
        }
        throw new Exception("");
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    /// <param name="gdmComplexType"></param>
    /// <returns></returns>
    public static bool TryGetGdmComplexType(this Type type, out IOGraphGdmComplexType? gdmComplexType)
    {
        gdmComplexType = null;
        // 1. Check that Type is not null
        if (type is null)
        {
            return false;
        }
        // 2. Check that the type is a reference type
        if (!type.IsClass)
        {
            return false;
        }
        // 3. Ensure class is NOT abstract
        if (type.IsAbstract)
        {
            return false;
        }
        // 4. Since a delegate are actually compiled into a class. Let's check that 
        if (type.IsSubclassOf(typeof(Delegate)))
        {
            return false;
        }
        // 5. Check if type has default constructor
        if (type.GetConstructor(Type.EmptyTypes) is null)
        {
            return false;
        }
        if (type.IsAssignableTo(typeof(string)))
        {
            return false;
        }

        var complexType = typeof(GdmComplexType<>).MakeGenericType(type);

        gdmComplexType = (Activator.CreateInstance(complexType) as IOGraphGdmComplexType)!;

        return true;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="ArgumentException"></exception>
    public static IEnumerable<GdmProperty> GetGdmComplexTypeProperties(this Type type)
    {
        // 1. Check that Type is not null
        if (type is null)
        {
            throw new ArgumentNullException(nameof(type));
        }
        // 2. Check that the type is a reference type
        if (!type.IsClass)
        {
            throw new ArgumentException("Invalid type. Complex types must be a class.");
        }
        // 3. Ensure class is NOT abstract
        if (type.IsAbstract)
        {
            throw new ArgumentException("Complex types cannot be abstract.");
        }
        // 4. Since a delegate are actually compiled into a class. Let's check that 
        if (type.IsSubclassOf(typeof(Delegate)))
        {
            throw new ArgumentException("Delegates are not allowed as complex types.");
        }
        // 5. Check if type has default constructor
        if (type.GetConstructor(Type.EmptyTypes) is null)
        {
            throw new ArgumentException($"The type {type.Name} does not have a default constructor. {type.Name}.ctor()");
        }

        var properties = type
            .GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Where(p => p.CanWrite && p.CanRead && p.GetIndexParameters().Length == 0);

        foreach (var propertyInfo in properties)
        {
            var property = new GdmProperty()
            {
                Label = propertyInfo.Name,
                PropertyInfo = propertyInfo,
                Getter = (instance) =>
                {
                    return propertyInfo.GetValue(instance, null);
                },
                Setter = (instance, value) =>
                {
                    propertyInfo.SetValue(instance, value, null);
                }
            };
            if (propertyInfo.PropertyType.TryGetGdmType(out var gdmType))
            {
                property.Type = new GdmTypeReference()
                {
                    Definition = gdmType
                };
            }

            yield return property;
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static IOGraphGdmPrimitiveType GetGdmPrimitiveType(this Type type)
    {
        if (type.TryGetGdmPrimitiveType(out var gdmPrimitiveType))
        {
            return gdmPrimitiveType;
        }
        throw new Exception("Couldn't identify type");
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    /// <param name="gdmPrimitiveType"></param>
    /// <returns></returns>
    public static bool TryGetGdmPrimitiveType(this Type type, out IOGraphGdmPrimitiveType gdmPrimitiveType)
    {
        gdmPrimitiveType = null;
        if (type == typeof(Uri))
        {
            gdmPrimitiveType = new GdmUriType();
            return true;
        }
        if (type == typeof(Byte))
        {
            gdmPrimitiveType = new GdmByteType();
            return true;
        }
        if (type == typeof(Char))
        {
            gdmPrimitiveType = new GdmCharType();
            return true;
        }
        if (type == typeof(Char?))
        {
            gdmPrimitiveType = new GdmNullCharType();
            return true;
        }
        if (type == typeof(DateTimeOffset))
        {
            gdmPrimitiveType = new GdmDateTimeOffsetType();
            return true;
        }
        if (type == typeof(DateTimeOffset?))
        {
            gdmPrimitiveType = new GdmNullDateTimeOffsetType();
            return true;
        }
        if (type == typeof(DateTime))
        {
            gdmPrimitiveType = new GdmDateTimeType();
            return true;
        }
        if (type == typeof(DateTime?))
        {
            gdmPrimitiveType = new GdmNullDateTimeType();
            return true;
        }
        if (type == typeof(DateOnly))
        {
            gdmPrimitiveType = new GdmDateType();
            return true;
        }
        if (type == typeof(DateOnly?))
        {
            gdmPrimitiveType = new GdmNullDateType();
            return true;
        }
        if (type == typeof(Decimal))
        {
            gdmPrimitiveType = new GdmDecimalType();
            return true;
        }
        if (type == typeof(Decimal?))
        {
            gdmPrimitiveType = new GdmNullDecimalType();
            return true;
        }
        if (type == typeof(Double))
        {
            gdmPrimitiveType = new GdmDoubleType();
            return true;
        }
        if (type == typeof(Double?))
        {
            gdmPrimitiveType = new GdmNullDoubleType();
            return true;
        }
        if (type == typeof(float))
        {
            gdmPrimitiveType = new GdmFloatType();
            return true;
        }
        if (type == typeof(float?))
        {
            gdmPrimitiveType = new GdmNullFloatType();
            return true;
        }
        if (type == typeof(Guid))
        {
            gdmPrimitiveType = new GdmGuidType();
            return true;
        }
        if (type == typeof(Guid?))
        {
            gdmPrimitiveType = new GdmNullGuidType();
            return true;
        }
        if (type == typeof(Half))
        {
            gdmPrimitiveType = new GdmHalfType();
            return true;
        }
        if (type == typeof(Half?))
        {
            gdmPrimitiveType = new GdmNullHalfType();
            return true;
        }
#if NET7_0_OR_GREATER
        if (type == typeof(Int128))
        {
            gdmPrimitiveType = new GdmInt128Type();
            return true;
        }
        if (type == typeof(Int128?))
        {
            gdmPrimitiveType = new GdmNullInt128Type();
            return true;

        }
#endif
        if (type == typeof(Int16))
        {
            gdmPrimitiveType = new GdmInt16Type();
            return true;
        }
        if (type == typeof(Int16?))
        {
            gdmPrimitiveType = new GdmNullInt16Type();
            return true;
        }
        if (type == typeof(Int32))
        {
            gdmPrimitiveType = new GdmInt32Type();
            return true;
        }
        if (type == typeof(Int32?))
        {
            gdmPrimitiveType = new GdmNullInt32Type();
            return true;
        }
        if (type == typeof(Int64))
        {
            gdmPrimitiveType = new GdmInt64Type();
            return true;
        }
        if (type == typeof(Int64?))
        {
            gdmPrimitiveType = new GdmNullInt64Type();
            return true;
        }
        if (type == typeof(string))
        {
            gdmPrimitiveType = new GdmStringType();
            return true;
        }
        if (type == typeof(TimeSpan))
        {
            gdmPrimitiveType = new GdmTimeSpanType();
            return true;
        }
        if (type == typeof(TimeSpan?))
        {
            gdmPrimitiveType = new GdmNullTimeSpanType();
            return true;
        }
        if (type == typeof(TimeOnly))
        {
            gdmPrimitiveType = new GdmTimeType();
            return true;
        }
        if (type == typeof(TimeOnly?))
        {
            gdmPrimitiveType = new GdmNullTimeType();
            return true;
        }
        if (type == typeof(UInt16))
        {
            gdmPrimitiveType = new GdmUInt16Type();
            return true;
        }
        if (type == typeof(UInt16?))
        {
            gdmPrimitiveType = new GdmNullUInt16Type();
            return true;
        }
        if (type == typeof(UInt32))
        {
            gdmPrimitiveType = new GdmUInt32Type();
            return true;
        }
        if (type == typeof(UInt32?))
        {
            gdmPrimitiveType = new GdmNullUInt32Type();
            return true;
        }
        if (type == typeof(UInt64))
        {
            gdmPrimitiveType = new GdmUInt64Type();
            return true;
        }
        if (type == typeof(UInt64?))
        {
            gdmPrimitiveType = new GdmNullUInt64Type();
            return true;
        }

        return false;
    }
}
