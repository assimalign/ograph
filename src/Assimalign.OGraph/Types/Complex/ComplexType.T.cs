using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Reflection;

namespace Assimalign.OGraph;

using Assimalign.OGraph.Internal;
using System.Collections;

public class ComplexType<T> : ComplexType
{
    public ComplexType()
    {
        base.RuntimeType    = typeof(T);
        base.TypeName       = typeof(T).Name;

        Configure(new OGraphComplexTypeDescriptor<T>(this));

        Initialize();
    }

    protected virtual void Configure(IOGraphComplexTypeDescriptor<T> descriptor) { }

    private void Initialize()
    {
        // Get all public runtimeProperties that are both read and write
        var runtimeProperties = typeof(T).GetProperties().Where(prop=> prop.CanWrite && prop.CanRead);

        foreach (var runtimeProperty in runtimeProperties)
        {
            if (Properties.TryGet(runtimeProperty.Name, out var property))
            {
                if (property is null)
                {
                    throw new Exception();
                }
                // Check if resolver was set
                if (property.Resolver is null)
                {
                    ((OGraphProperty)property).Resolver = GetPropertyResolver(runtimeProperty);
                }
            }
            else
            {
                Properties.Add(GetProperty(runtimeProperty));
            }
        }
    }

    private IOGraphProperty GetProperty(PropertyInfo propertyInfo)
    {
        if (propertyInfo.PropertyType.IsValueType(out var valueType))
        {
            return valueType.Name switch
            {
                nameof(Guid) => GetGuidProperty(propertyInfo),
                nameof(Boolean) => GetBooleanProperty(propertyInfo),
                nameof(Int16) => GetInt16Property(propertyInfo),
                nameof(Int32) => GetInt32Property(propertyInfo),
                nameof(Int64) => GetInt64Property(propertyInfo),
                nameof(UInt16) => GetUInt16Property(propertyInfo),
                nameof(UInt32) => GetUInt32Property(propertyInfo),
                nameof(DateOnly) => GetDateProperty(propertyInfo),
                nameof(DateTime) => GetDateTimeProperty(propertyInfo),
                nameof(DateTimeOffset) => GetDateTimeOffsetProperty(propertyInfo),
                nameof(Byte) => GetByteProperty(propertyInfo),
                nameof(Char) => GetCharProperty(propertyInfo),
                nameof(Decimal) => GetDecimalProperty(propertyInfo),
                nameof(Double) => GetDoubleProperty(propertyInfo),
                nameof(Single) => GetFloatProperty(propertyInfo),
                _ => throw new Exception()
            };
        }
        if (propertyInfo.PropertyType.IsStringType())
        {
            return GetStringProperty(propertyInfo);
        }
        if (propertyInfo.PropertyType.IsEnumerableType(out var enumerableType)) 
        {
            if (enumerableType.IsComplexType())
            {
                var collectionType = typeof(CollectionType<>).MakeGenericType(
                    typeof(ComplexType<>).MakeGenericType(propertyInfo.PropertyType));
                var collectionInstance = Activator.CreateInstance(collectionType) as IOGraphCollectionType;


                var complexType = typeof(ComplexType<>).MakeGenericType(enumerableType);
                var complexInstance = Activator.CreateInstance(complexType) as IOGraphComplexType;

                var collectionTypeProperty = collectionType.GetProperty(nameof(CollectionType<ComplexType>.ItemType));

                collectionTypeProperty.SetValue(collectionInstance, complexInstance, null);


                return new OGraphProperty()
                {
                    Name = propertyInfo.Name,
                    Type = collectionInstance,
                    Resolver = GetPropertyResolver(propertyInfo)
                };
            }
        }
        if (propertyInfo.PropertyType.IsComplexType())
        {
            var complexType = typeof(ComplexType<>).MakeGenericType(propertyInfo.PropertyType);
            var complexInstance = Activator.CreateInstance(complexType) as IOGraphComplexType;

            return new OGraphProperty()
            {
                Name = propertyInfo.Name,
                Type = complexInstance,
                Resolver = GetPropertyResolver(propertyInfo)
            };
        }
        throw new Exception();
    }
    private IOGraphProperty GetGuidProperty(PropertyInfo propertyInfo)
    {
        return new OGraphProperty()
        {
            Name = propertyInfo.Name,
            Type = new GuidType()
            {
                IsNullable = propertyInfo.PropertyType.IsNullable(),
            },
            Resolver = GetPropertyResolver(propertyInfo)
        };
    }
    private IOGraphProperty GetStringProperty(PropertyInfo propertyInfo)
    {
        return new OGraphProperty()
        {
            Name = propertyInfo.Name,
            Type = new StringType(),
            Resolver = GetPropertyResolver(propertyInfo)
        };
    }
    private IOGraphProperty GetInt16Property(PropertyInfo propertyInfo)
    {
        return new OGraphProperty()
        {
            Name = propertyInfo.Name,
            Type = new ShortType()
            {
                IsNullable = propertyInfo.PropertyType.IsNullable(),
            },
            Resolver = GetPropertyResolver(propertyInfo)
        };
    }
    private IOGraphProperty GetInt32Property(PropertyInfo propertyInfo)
    {
        return new OGraphProperty()
        {
            Name = propertyInfo.Name,
            Type = new IntType()
            {
                IsNullable = propertyInfo.PropertyType.IsNullable(),
            },
            Resolver = GetPropertyResolver(propertyInfo)
        };
    }
    private IOGraphProperty GetInt64Property(PropertyInfo propertyInfo)
    {
        return new OGraphProperty()
        {
            Name = propertyInfo.Name,
            Type = new LongType()
            {
                IsNullable = propertyInfo.PropertyType.IsNullable(),
            },
            Resolver = GetPropertyResolver(propertyInfo)
        };
    }
    private IOGraphProperty GetUInt16Property(PropertyInfo propertyInfo)
    {
        return new OGraphProperty()
        {
            Name = propertyInfo.Name,
            Type = new UShortType()
            {
                IsNullable = propertyInfo.PropertyType.IsNullable(),
            },
            Resolver = GetPropertyResolver(propertyInfo)
        };
    }
    private IOGraphProperty GetUInt32Property(PropertyInfo propertyInfo)
    {
        return new OGraphProperty()
        {
            Name = propertyInfo.Name,
            Type = new UIntType()
            {
                IsNullable = propertyInfo.PropertyType.IsNullable(),
            },
            Resolver = GetPropertyResolver(propertyInfo)
        };
    }
    private IOGraphProperty GetUInt64Property(PropertyInfo propertyInfo)
    {
        return new OGraphProperty()
        {
            Name = propertyInfo.Name,
            Type = new ULongType()
            {
                IsNullable = propertyInfo.PropertyType.IsNullable(),
            },
            Resolver = GetPropertyResolver(propertyInfo)
        };
    }
    private IOGraphProperty GetBooleanProperty(PropertyInfo propertyInfo)
    {
        return new OGraphProperty()
        {
            Name = propertyInfo.Name,
            Type = new BooleanType()
            {
                IsNullable = propertyInfo.PropertyType.IsNullable(),
            },
            Resolver = GetPropertyResolver(propertyInfo)
        };
    }
    private IOGraphProperty GetDateProperty(PropertyInfo propertyInfo)
    {
        return new OGraphProperty()
        {
            Name = propertyInfo.Name,
            Type = new DateType()
            {
                IsNullable = propertyInfo.PropertyType.IsNullable(),
            },
            Resolver = GetPropertyResolver(propertyInfo)
        };
    }
    private IOGraphProperty GetDateTimeProperty(PropertyInfo propertyInfo)
    {
        return new OGraphProperty()
        {
            Name = propertyInfo.Name,
            Type = new DateTimeType()
            {
                IsNullable = propertyInfo.PropertyType.IsNullable(),
            },
            Resolver = GetPropertyResolver(propertyInfo)
        };
    }
    private IOGraphProperty GetDateTimeOffsetProperty(PropertyInfo propertyInfo)
    {
        return new OGraphProperty()
        {
            Name = propertyInfo.Name,
            Type = new DateTimeOffsetType()
            {
                IsNullable = propertyInfo.PropertyType.IsNullable(),
            },
            Resolver = GetPropertyResolver(propertyInfo)
        };
    }
    private IOGraphProperty GetByteProperty(PropertyInfo propertyInfo)
    {
        return new OGraphProperty()
        {
            Name = propertyInfo.Name,
            Type = new ByteType()
            {
                IsNullable = propertyInfo.PropertyType.IsNullable(),
            },
            Resolver = GetPropertyResolver(propertyInfo)
        };
    }
    private IOGraphProperty GetCharProperty(PropertyInfo propertyInfo)
    {
        return new OGraphProperty()
        {
            Name = propertyInfo.Name,
            Type = new CharType()
            {
                IsNullable = propertyInfo.PropertyType.IsNullable(),
            },
            Resolver = GetPropertyResolver(propertyInfo)
        };
    }
    private IOGraphProperty GetDecimalProperty(PropertyInfo propertyInfo)
    {
        return new OGraphProperty()
        {
            Name = propertyInfo.Name,
            Type = new DecimalType()
            {
                IsNullable = propertyInfo.PropertyType.IsNullable(),
            },
            Resolver = GetPropertyResolver(propertyInfo)
        };
    }
    private IOGraphProperty GetDoubleProperty(PropertyInfo propertyInfo)
    {
        return new OGraphProperty()
        {
            Name = propertyInfo.Name,
            Type = new DoubleType()
            {
                IsNullable = propertyInfo.PropertyType.IsNullable(),
            },
            Resolver = GetPropertyResolver(propertyInfo)
        };

    }
    private IOGraphProperty GetFloatProperty(PropertyInfo propertyInfo)
    {
        return new OGraphProperty()
        {
            Name = propertyInfo.Name,
            Type = new FloatType()
            {
                IsNullable = propertyInfo.PropertyType.IsNullable(),
            },
            Resolver = GetPropertyResolver(propertyInfo)
        };
    }
    private IOGraphPropertyResolver GetPropertyResolver(PropertyInfo propertyInfo)
    {
        var parameterExpression = Expression.Parameter(typeof(T));
        var memberExpression = Expression.Property(parameterExpression, propertyInfo);
        var labdaExpression = Expression.Lambda(memberExpression, parameterExpression);
        var method = labdaExpression.Compile();

        return new OGraphPropertyResolverDefault(context =>
        {
            var parent = context.GetParent<T>();

            return ValueTask.FromResult<IOGraphPropertyResult>(new OGraphPropertyResult()
            {
                Value = method.DynamicInvoke(parent)
            });
        });
    }
}
