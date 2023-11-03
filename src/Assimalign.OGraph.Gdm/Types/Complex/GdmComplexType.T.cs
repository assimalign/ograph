using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Reflection;

namespace Assimalign.OGraph.Gdm;

using Assimalign.OGraph.Gdm.Internal;

public partial class ComplexType<T> : GdmComplexType
    where T : class, new()
{
    public ComplexType()
    {
        base.RuntimeType = typeof(T);
        base.Label = typeof(T).Name;
        Initialize();
        Configure(new GdmComplexTypeDescriptor<T>(this));
    }

    protected virtual void Configure(IOGraphGdmComplexTypeDescriptor<T> descriptor) { }

    private void Initialize()
    {
        // Get all public runtimeProperties that are both read and write
        var props = typeof(T).GetProperties().Where(prop => prop.CanWrite && prop.CanRead);

        foreach (var prop in props)
        {
            var propName = prop.Name;
            var propType = prop.PropertyType;
            var propTypeArgs = prop.PropertyType.GetGenericArguments();

            // Let's do a check for nullable type and pull it out
            if (propType.IsValueType) /// Enum's are also value types
            {
                if (propTypeArgs.Length == 1)
                {
                    var nullType = typeof(Nullable<>).MakeGenericType(propTypeArgs[0]);
                    if (nullType.IsAssignableTo(propType))
                    {
                        propType = propTypeArgs[0];
                    }
                }
            }
            if (propType.IsEnum)
            {
                var enumType = typeof(GdmEnumType<>).MakeGenericType(propType);
                var enumTypeReference = typeof(GdmTypeReference<>).MakeGenericType(enumType);
                var enumRefObj = Activator.CreateInstance(enumTypeReference) as IOGraphGdmTypeReference;

                Properties.Add(new GdmProperty()
                {
                    Name = propName,
                    Type = enumRefObj!,
                    //Resolver = GetPropertyResolver(prop)
                });
                continue;
            }
            if (propType.IsValueType || propType == typeof(string))
            {
                Properties.Add(new GdmProperty()
                {
                    Name = propName,
                    Type = new GdmTypeReference()
                    {
                        Definition = GetPrimitiveType(propType)
                    },
                    //Resolver = GetPropertyResolver(prop)
                });
                continue;
            }
            if (propType.IsEnumerableType(out var enumerableType))
            {
                Properties.Add(new GdmProperty()
                {
                    Name = propName,
                    Type = new GdmTypeReference()
                    {
                        Definition = GetCollectionType(enumerableType)
                    },
                    //Resolver = GetPropertyResolver(prop)
                });
                continue;
            }
            if (propType.IsComplexType())
            {
                var complexType = typeof(ComplexType<>).MakeGenericType(propType);
                var complexObj = Activator.CreateInstance(complexType) as IOGraphGdmType;

                Properties.Add(new GdmProperty()
                {
                    Name = propName,
                    Type = new GdmTypeReference()
                    {
                        Definition = complexObj
                    },
                    //Resolver = GetPropertyResolver(prop)
                });
                continue;
            }

            //throw new Exception($"The following property: '{prop.Name}' on type '{prop.DeclaringType.Name}' has an unsupported type.");
        }
    }

    private IOGraphGdmType GetCollectionType(Type type)
    {
        var propType = type;
        var propTypeArgs = type.GetGenericArguments();

        if (propType.IsValueType) /// Enum's are also value types
        {
            if (propTypeArgs.Length == 1)
            {
                var nullType = typeof(Nullable<>).MakeGenericType(propTypeArgs[0]);
                if (nullType.IsAssignableTo(propType))
                {
                    propType = propTypeArgs[0];
                }
            }
        }
        if (propType.IsEnum)
        {
            var collectionType = typeof(GdmCollectionType<>).MakeGenericType(
                typeof(GdmEnumType<>).MakeGenericType(propType));

            return (Activator.CreateInstance(collectionType) as IOGraphGdmType)!;
        }
        if (propType.IsValueType || propType == typeof(string))
        {
            var collectionType = typeof(GdmCollectionType<>).MakeGenericType(
                GetPrimitiveType(propType).GetType().MakeGenericType(propType));

            return (Activator.CreateInstance(collectionType) as IOGraphGdmType)!;
        }
        if (propType.IsEnumerableType(out var enumerableType))
        {
            var collectionType = typeof(GdmCollectionType<>).MakeGenericType(
                typeof(GdmCollectionType<>).MakeGenericType(
                    GetCollectionType(enumerableType).GetType()));

            return (Activator.CreateInstance(collectionType) as IOGraphGdmType)!;
        }
        if (propType.IsComplexType())
        {
            var collectionType = typeof(GdmCollectionType<>).MakeGenericType(
                typeof(ComplexType<>).MakeGenericType(propType));

            return (Activator.CreateInstance(collectionType) as IOGraphGdmType)!;
        }
        throw new Exception("");
    }

    private IOGraphGdmType GetPrimitiveType(Type type) => type.Name switch
    {
        nameof(Guid) => new GdmGuidType(),
        //nameof(Boolean) => new GdmBooleanType(),
        nameof(Int16) => new GdmInt16Type(),
        nameof(Int32) => new GdmInt32Type(),
        nameof(Int64) => new GdmInt64Type(),
        nameof(UInt16) => new GdmUInt16Type(),
        nameof(UInt32) => new GdmUInt32Type(),
        nameof(DateOnly) => new GdmDateType(),
        nameof(DateTime) => new GdmDateTimeType(),
        nameof(DateTimeOffset) => new GdmDateTimeOffsetType(),
        nameof(Byte) => new GdmByteType(),
        nameof(Char) => new GdmCharType(),
        nameof(Decimal) => new GdmDecimalType(),
        nameof(Double) => new GdmDoubleType(),
        nameof(Single) => new GdmFloatType(),
        nameof(String) => new GdmStringType(),
        _ => throw new Exception("")
    };

    //private IOGraphPropertyResolver GetPropertyResolver(PropertyInfo propertyInfo)
    //{
    //    var parameter = Expression.Parameter(typeof(T));
    //    var member = Expression.Property(parameter, propertyInfo);
    //    var lambda = Expression.Lambda(member, parameter);
    //    var method = lambda.Compile();

    //    return new PropertyResolverDefault((context, cancellationToken) =>
    //    {
    //        var parent = context.GetParent<T>();

    //        return ValueTask.FromResult<IOGraphResult>(new PropertyResult()
    //        {
    //            Value = method!.DynamicInvoke(parent)
    //        });
    //    });
    //}
}