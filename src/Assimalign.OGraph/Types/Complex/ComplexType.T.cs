using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Reflection;

namespace Assimalign.OGraph;

using Assimalign.OGraph.Internal;

public partial class ComplexType<T> : ComplexType
    where T : class, new()
{
    public ComplexType()
    {
        base.RuntimeType    = typeof(T);
        base.Name           = typeof(T).Name;
        Initialize(); 
        Configure(new OGraphComplexTypeDescriptor<T>(this));
    }

    protected virtual void Configure(IOGraphComplexTypeDescriptor<T> descriptor) { }

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
                var enumType = typeof(EnumType<>).MakeGenericType(propType);
                var enumObj = Activator.CreateInstance(enumType) as IOGraphType;

                Properties.Add(new Property()
                {
                    Name = propName,
                    Type = enumObj,
                    Resolver = GetPropertyResolver(prop)
                });
                continue;
            }
            if (propType.IsValueType || propType == typeof(string))
            {
                Properties.Add(new Property()
                {
                    Name = propName,
                    Type = GetPrimitiveType(propType),
                    Resolver = GetPropertyResolver(prop)
                });
                continue;
            }
            if (propType.IsEnumerableType(out var enumerableType))
            {
                Properties.Add(new Property()
                {
                    Name = propName,
                    Type = GetCollectionType(enumerableType),
                    Resolver = GetPropertyResolver(prop)
                });
                continue;
            }
            if (propType.IsComplexType())
            {
                var complexType = typeof(ComplexType<>).MakeGenericType(propType);
                var complexObj = Activator.CreateInstance(complexType) as IOGraphType;

                Properties.Add(new Property()
                {
                    Name = propName,
                    Type =complexObj,
                    Resolver = GetPropertyResolver(prop)
                });
                continue;
            }

            //throw new Exception($"The following property: '{prop.Name}' on type '{prop.DeclaringType.Name}' has an unsupported type.");
        }
    }

    private IOGraphType GetCollectionType(Type type)
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
            var collectionType = typeof(CollectionType<>).MakeGenericType(
                typeof(EnumType<>).MakeGenericType(propType));

            return (Activator.CreateInstance(collectionType) as IOGraphType)!;
        }
        if (propType.IsValueType || propType == typeof(string))
        {
            var collectionType = typeof(CollectionType<>).MakeGenericType(
                GetPrimitiveType(propType).GetType().MakeGenericType(propType));

            return (Activator.CreateInstance(collectionType) as IOGraphType)!;
        }
        if (propType.IsEnumerableType(out var enumerableType))
        {
            var collectionType = typeof(CollectionType<>).MakeGenericType(
                typeof(CollectionType<>).MakeGenericType(
                    GetCollectionType(enumerableType).GetType()));

            return (Activator.CreateInstance(collectionType) as IOGraphType)!;
        }
        if (propType.IsComplexType())
        {
            var collectionType = typeof(CollectionType<>).MakeGenericType(
                typeof(ComplexType<>).MakeGenericType(propType));

            return (Activator.CreateInstance(collectionType) as IOGraphType)!;
        }
        throw new Exception("");
    }

    private IOGraphType GetPrimitiveType(Type type) => type.Name switch
    {
        nameof(Guid) => new GuidType(),
        nameof(Boolean) => new BooleanType(),
        nameof(Int16) => new ShortType(),
        nameof(Int32) => new IntType(),
        nameof(Int64) =>new LongType(),
        nameof(UInt16) => new UShortType(),
        nameof(UInt32) => new UIntType(),
        nameof(DateOnly) => new DateType(),
        nameof(DateTime) => new DateTimeType(),
        nameof(DateTimeOffset) => new DateTimeOffsetType(),
        nameof(Byte) => new ByteType(),
        nameof(Char) => new CharType(),
        nameof(Decimal) => new DecimalType(),
        nameof(Double) => new DoubleType(),
        nameof(Single) => new FloatType(),
        nameof(String) => new StringType(),
        _ => throw new Exception("")
    };

    private IOGraphPropertyResolver GetPropertyResolver(PropertyInfo propertyInfo)
    {
        var parameter = Expression.Parameter(typeof(T));
        var member = Expression.Property(parameter, propertyInfo);
        var lambda = Expression.Lambda(member, parameter);
        var method = lambda.Compile();

        return new PropertyResolverDefault((context, cancellationToken) =>
        {
            var parent = context.GetParent<T>();

            return ValueTask.FromResult<IOGraphResult>(new PropertyResult()
            {
                Value = method!.DynamicInvoke(parent)
            });
        });
    }
}