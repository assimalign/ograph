using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

namespace Assimalign.OGraph.Gdm.Internal;

internal static class GdmTypeExtensions
{
    public static GdmProperty GetProperty(this IOGraphGdmComplexType complexType, PropertyInfo propertyInfo)
    {
        return complexType.Properties
            .Select(p => p is GdmProperty property ? property : complexType.WrapProperty(p, propertyInfo))
            .FirstOrDefault(p => p.PropertyInfo.Name == propertyInfo.Name) ?? new GdmProperty()
            {
                PropertyInfo = propertyInfo,
                Label = propertyInfo.Name,
            };
    }
    public static GdmProperty WrapProperty(this IOGraphGdmComplexType complexType, IOGraphGdmProperty property, PropertyInfo propertyInfo)
    {
        complexType.Properties.Remove(property);

        var wrapped = new GdmProperty()
        {
            IsComputed = property.IsComputed,
            Getter = property.Getter,
            Setter = property.Setter,
            PropertyInfo = propertyInfo,
            IsNullable = property.IsNullable,
            Metadata = property.Metadata,
            Label = property.Label,
            Type = property.Type
        };

        complexType.Properties.Add(wrapped);

        return wrapped;
    }




//    public static IOGraphGdmType GetGdmType([DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] this Type runtimeType)
//    {
//        if (!runtimeType.TryGetGdmType(out var gdmType))
//        {
//            GdmThrowHelper.ThrowArgumentException("Runtime type could not be mapped to GDM Type.");
//        }
//        return gdmType!;
//    }
//    public static bool TryGetGdmType([DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor)] this Type runtimeType, out IOGraphGdmType? gdmType)
//    {
//        gdmType = null;

//        if (runtimeType.TryGetGdmPrimitiveType(out var gdmPrimitiveType))
//        {
//            gdmType = gdmPrimitiveType;
//            return true;
//        }
//        if (runtimeType.TryGetGdmEnumType(out var gdmEnumType))
//        {
//            gdmType = gdmEnumType;
//            return true;
//        }
//        if (runtimeType.TryGetGdmCollectionType(out var gdmCollectionType))
//        {
//            gdmType = gdmCollectionType;
//            return true;
//        }
//        if (runtimeType.TryGetGdmComplexType(out var gdmComplexType))
//        {
//            gdmType = gdmComplexType;
//            return true;
//        }

//        return false;
//    }
//    public static IOGraphGdmEnumType GetGdmEnumType(this Type runtimeType)
//    {
//        if (runtimeType.TryGetGdmEnumType(out var gdmType))
//        {
//            GdmThrowHelper.ThrowArgumentException("Runtime type could not be mapped to GDM Type.");
//        }
//        return gdmType!;
//    }
//    public static bool TryGetGdmEnumType(this Type runtimeType, out IOGraphGdmEnumType? gdmType)
//    {
//        gdmType = null;

//        if (runtimeType.Name == "Nullable`1" && runtimeType.GenericTypeArguments[0].IsEnum)
//        {
//            gdmType = new GdmEnumType(runtimeType.GenericTypeArguments[0]);
//        }
//        if (runtimeType.IsEnum)
//        {

//        }


//        if (runtimeType.IsValueType && 
//            !runtimeType.IsEnum && 
//            runtimeType.GenericTypeArguments.Length == 1 && runtimeType.Ge)
//        runtimeType.GenericTypeArguments


//        //if (type.IsEnum)
//        //{
//        //    var enumType = typeof(GdmEnumType<>).MakeGenericType(type);
//        //    gdmEnumType = (Activator.CreateInstance(enumType) as IOGraphGdmEnumType)!;
//        //    return true;
//        //}
//        //var typeArgs = type.GetGenericArguments();
//        //if (typeArgs.Length == 1 &&
//        //    typeArgs[0].IsEnum &&
//        //    typeof(Nullable<>).MakeGenericType(typeArgs[0]).IsAssignableTo(type))
//        //{
//        //    var enumType = typeof(GdmNullEnumType<>).MakeGenericType(typeArgs[0]);
//        //    gdmEnumType = (Activator.CreateInstance(enumType) as IOGraphGdmEnumType)!;
//        //    return true;
//        //}

//        return false;
//    }
//    /// <summary>
//    /// 
//    /// </summary>
//    /// <param name="type"></param>
//    /// <returns></returns>
//    public static IOGraphGdmCollectionType GetGdmCollectionType(this Type type)
//    {
//        if (type.TryGetGdmCollectionType(out var gdmCollectionType))
//        {
//            return gdmCollectionType!;
//        }
//        throw new Exception();
//    }
//    public static bool TryGetGdmCollectionType(this Type runtimeType, out IOGraphGdmCollectionType? gdmType)
//    {
//        gdmType = null;

//        if (runtimeType.IsGenericType)
//        {

//        }

//        if (runtimeType.IsEnumerableType(out var enumerableType))
//        {
            
//            if (enumerableType.TryGetGdmType(out var gdmType))
//            {
//                var collectionType = typeof(GdmListType<>).MakeGenericType(gdmType!.GetType());
//                gdmType = (Activator.CreateInstance(collectionType) as IOGraphGdmCollectionType)!;
//                return true;
//            }
//        }

//        return false;
//    }

//    public static IOGraphGdmComplexType GetGdmComplexType([DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] this Type type)
//    {
//        if (type.TryGetGdmComplexType(out var gdmComplexType))
//        {
//            return gdmComplexType!;
//        }
//        throw new Exception("");
//    }
//    public static bool TryGetGdmComplexType([DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] this Type runtimeType, out IOGraphGdmComplexType? gdmType)
//    {
//        gdmType = null;
//        // 1. Check that Type is not null
//        if (runtimeType is null)
//        {
//            return false;
//        }
//        // 2. Check that the type is a reference type
//        if (!runtimeType.IsClass)
//        {
//            return false;
//        }
//        // 3. Ensure class is NOT abstract
//        if (runtimeType.IsAbstract)
//        {
//            return false;
//        }
//        // 4. Since a delegate are actually compiled into a class. Let's check that 
//        if (runtimeType.IsSubclassOf(typeof(Delegate)))
//        {
//            return false;
//        }
//        // 5. Check if type has default constructor
//        if (runtimeType.GetConstructor(Type.EmptyTypes) is null)
//        {
//            return false;
//        }
//        // 6. Ensure 
//        if (runtimeType.IsAssignableTo(typeof(string)))
//        {
//            return false;
//        }


//        gdmType = GdmComplexType.Create(descriptor =>
//        {
//            descriptor.HasRuntimeType(runtimeType);
//        });
//        //var complexType = typeof(GdmComplexType<>).MakeGenericType(type);

//        //gdmComplexType = (Activator.CreateInstance(complexType) as IOGraphGdmComplexType)!;

//        return true;
//    }

//    public static IEnumerable<GdmProperty> GetGdmComplexTypeProperties(this Type type)
//    {
//        // 1. Check that Type is not null
//        if (type is null)
//        {
//            throw new ArgumentNullException(nameof(type));
//        }
//        // 2. Check that the type is a reference type
//        if (!type.IsClass)
//        {
//            throw new ArgumentException("Invalid type. Complex types must be a class.");
//        }
//        // 3. Ensure class is NOT abstract
//        if (type.IsAbstract)
//        {
//            throw new ArgumentException("Complex types cannot be abstract.");
//        }
//        // 4. Since a delegate are actually compiled into a class. Let's check that 
//        if (type.IsSubclassOf(typeof(Delegate)))
//        {
//            throw new ArgumentException("Delegates are not allowed as complex types.");
//        }
//        // 5. Check if type has default constructor
//        if (type.GetConstructor(Type.EmptyTypes) is null)
//        {
//            throw new ArgumentException($"The type {type.Name} does not have a default constructor. {type.Name}.ctor()");
//        }

//        var properties = type
//            .GetProperties(BindingFlags.Public | BindingFlags.Instance)
//            .Where(p => p.CanWrite && p.CanRead && p.GetIndexParameters().Length == 0);

//        foreach (var propertyInfo in properties)
//        {
//            var property = new GdmProperty()
//            {
//                Label = propertyInfo.Name,
//                PropertyInfo = propertyInfo,
//                Getter = (instance) =>
//                {
//                    return propertyInfo.GetValue(instance, null);
//                },
//                Setter = (instance, value) =>
//                {
//                    propertyInfo.SetValue(instance, value, null);
//                }
//            };
//            if (propertyInfo.PropertyType.TryGetGdmType(out var gdmType))
//            {
//                property.Type = new GdmTypeReference()
//                {
//                    Definition = gdmType!
//                };
//            }

//            yield return property;
//        }
//    }

//    public static IOGraphGdmPrimitiveType GetGdmPrimitiveType(this Type type)
//    {
//        if (type.TryGetGdmPrimitiveType(out var gdmPrimitiveType))
//        {
//            return gdmPrimitiveType!;
//        }
//        throw new Exception("Couldn't identify type");
//    }
//    public static bool TryGetGdmPrimitiveType(this Type runtimeType, out IOGraphGdmPrimitiveType? gdmType)
//    {
//        gdmType = null;

//        static Dictionary<Type, IOGraphGdmType> types = new();
//        if (runtimeType == typeof(Uri))
//        {
//            gdmType = new GdmUriType();
//            return true;
//        }
//        if (runtimeType == typeof(Byte))
//        {
//            gdmType = new GdmByteType();
//            return true;
//        }
//        if (runtimeType == typeof(Char))
//        {
//            gdmType = new GdmCharType();
//            return true;
//        }
//        if (runtimeType == typeof(Char?))
//        {
//            gdmType = new GdmNullCharType();
//            return true;
//        }
//        if (runtimeType == typeof(DateTimeOffset))
//        {
//            gdmType = new GdmDateTimeOffsetType();
//            return true;
//        }
//        if (runtimeType == typeof(DateTimeOffset?))
//        {
//            gdmType = new GdmNullDateTimeOffsetType();
//            return true;
//        }
//        if (runtimeType == typeof(DateTime))
//        {
//            gdmType = new GdmDateTimeType();
//            return true;
//        }
//        if (runtimeType == typeof(DateTime?))
//        {
//            gdmType = new GdmNullDateTimeType();
//            return true;
//        }
//        if (runtimeType == typeof(DateOnly))
//        {
//            gdmType = new GdmDateType();
//            return true;
//        }
//        if (runtimeType == typeof(DateOnly?))
//        {
//            gdmType = new GdmNullDateType();
//            return true;
//        }
//        if (runtimeType == typeof(Decimal))
//        {
//            gdmType = new GdmDecimalType();
//            return true;
//        }
//        if (runtimeType == typeof(Decimal?))
//        {
//            gdmType = new GdmNullDecimalType();
//            return true;
//        }
//        if (runtimeType == typeof(Double))
//        {
//            gdmType = new GdmDoubleType();
//            return true;
//        }
//        if (runtimeType == typeof(Double?))
//        {
//            gdmType = new GdmNullDoubleType();
//            return true;
//        }
//        if (runtimeType == typeof(float))
//        {
//            gdmType = new GdmFloatType();
//            return true;
//        }
//        if (runtimeType == typeof(float?))
//        {
//            gdmType = new GdmNullFloatType();
//            return true;
//        }
//        if (runtimeType == typeof(Guid))
//        {
//            gdmType = new GdmGuidType();
//            return true;
//        }
//        if (runtimeType == typeof(Guid?))
//        {
//            gdmType = new GdmNullGuidType();
//            return true;
//        }
//        if (runtimeType == typeof(Half))
//        {
//            gdmType = new GdmHalfType();
//            return true;
//        }
//        if (runtimeType == typeof(Half?))
//        {
//            gdmType = new GdmNullHalfType();
//            return true;
//        }
//#if NET7_0_OR_GREATER
//        if (runtimeType == typeof(Int128))
//        {
//            gdmType = new GdmInt128Type();
//            return true;
//        }
//        if (runtimeType == typeof(Int128?))
//        {
//            gdmType = new GdmNullInt128Type();
//            return true;

//        }
//#endif
//        if (runtimeType == typeof(Int16))
//        {
//            gdmType = new GdmInt16Type();
//            return true;
//        }
//        if (runtimeType == typeof(Int16?))
//        {
//            gdmType = new GdmNullInt16Type();
//            return true;
//        }
//        if (runtimeType == typeof(Int32))
//        {
//            gdmType = new GdmInt32Type();
//            return true;
//        }
//        if (runtimeType == typeof(Int32?))
//        {
//            gdmType = new GdmNullInt32Type();
//            return true;
//        }
//        if (runtimeType == typeof(Int64))
//        {
//            gdmType = new GdmInt64Type();
//            return true;
//        }
//        if (runtimeType == typeof(Int64?))
//        {
//            gdmType = new GdmNullInt64Type();
//            return true;
//        }
//        if (runtimeType == typeof(string))
//        {
//            gdmType = new GdmStringType();
//            return true;
//        }
//        if (runtimeType == typeof(TimeSpan))
//        {
//            gdmType = new GdmTimeSpanType();
//            return true;
//        }
//        if (runtimeType == typeof(TimeSpan?))
//        {
//            gdmType = new GdmNullTimeSpanType();
//            return true;
//        }
//        if (runtimeType == typeof(TimeOnly))
//        {
//            gdmType = new GdmTimeType();
//            return true;
//        }
//        if (runtimeType == typeof(TimeOnly?))
//        {
//            gdmType = new GdmNullTimeType();
//            return true;
//        }
//        if (runtimeType == typeof(UInt16))
//        {
//            gdmType = new GdmUInt16Type();
//            return true;
//        }
//        if (runtimeType == typeof(UInt16?))
//        {
//            gdmType = new GdmNullUInt16Type();
//            return true;
//        }
//        if (runtimeType == typeof(UInt32))
//        {
//            gdmType = new GdmUInt32Type();
//            return true;
//        }
//        if (runtimeType == typeof(UInt32?))
//        {
//            gdmType = new GdmNullUInt32Type();
//            return true;
//        }
//        if (runtimeType == typeof(UInt64))
//        {
//            gdmType = new GdmUInt64Type();
//            return true;
//        }
//        if (runtimeType == typeof(UInt64?))
//        {
//            gdmType = new GdmNullUInt64Type();
//            return true;
//        }

//        return false;
//    }





}