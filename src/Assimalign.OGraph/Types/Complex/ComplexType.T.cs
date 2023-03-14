using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Reflection;

namespace Assimalign.OGraph;

using Assimalign.OGraph.Internal;


public class ComplexType<T> : ComplexType
{
    public ComplexType()
    {
        base.RuntimeType    = typeof(T);
        base.TypeName       = typeof(T).Name;

        Initialize();
    }

    protected virtual void Configure(IOGraphComplexTypeDescriptor<T> descriptor) { }

    private void Initialize()
    {
        // Get all public properties that are both read and write
        var properties = typeof(T).GetProperties().Where(prop=> prop.CanWrite && prop.CanRead);

        foreach (var property in properties)
        {
            if (property.PropertyType.IsValueType())
            {
                base.Properties.Add(new OGraphProperty()
                {
                    Name = property.Name,

                });
            }
            if (property.PropertyType.IsStringType())
            {
                base.Properties.Add(new OGraphProperty()
                {
                    Name = property.Name,
                    Type = new StringType(),
                    IsFilterable = true,
                    Resolver = GetResolver(property)
                });
            }
            if (property.PropertyType.IsComplexType())
            {
                var propertyType = property.PropertyType;
                var complexType = typeof(ComplexType<>).MakeGenericType(propertyType);
                var complexInstance = Activator.CreateInstance(complexType);

                if (complexInstance is not IOGraphComplexType type)
                {
                    throw new Exception();
                }
                base.Properties.Add(new OGraphProperty()
                {
                    Type = type,
                    Name = property.Name,
                    Resolver = GetResolver(property)
                });
            }
        }

        Configure(new OGraphComplexTypeDescriptor<T>(this));
    }
    
    private IOGraphPropertyResolver GetResolver(PropertyInfo propertyInfo)
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
                Data = method.DynamicInvoke(parent)
            });
        });
    }
}
