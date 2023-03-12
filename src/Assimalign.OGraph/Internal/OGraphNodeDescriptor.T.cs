using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Internal;

internal class OGraphNodeDescriptor<T> : IOGraphNodeDescriptor<T>
{
    private readonly IOGraphNode node;

    public OGraphNodeDescriptor(IOGraphNode node)
    {
        if (node is null)
        {
            throw new ArgumentNullException(nameof(node));
        }

        this.node = node;

        OnInitialize(node.Properties, typeof(T));
    }


   

    public IOGraphNodeDescriptor<T> HasLabel(Label label)
    {
        var property = node.GetType().GetProperty("Label");

        if (property is null)
        {
            throw new Exception();
        }
        if (!property.CanWrite)
        {
            throw new Exception();
        }

        property.SetValue(node, label, null);

        return this;
    }
    public IOGraphNodeDescriptor<T> HasMetadata(string key, object value)
    {
        if (key is null)
        {
            throw new ArgumentNullException(nameof(key));
        }
        if (value is null)
        {
            throw new ArgumentNullException(nameof(value));
        }

        node.Metadata[key] = value;

        return this;
    }
    public IOGraphEdgeDescriptor<TProperty> HasEdge<TProperty>(Expression<Func<T, TProperty>> expression)
    {
        ValidateMemberExpression(expression, out var memberInfo);

        // TODO: Need to remove existing from initialized collection 

        if (node.Properties.TryGet(memberInfo.Name, out var existing) && existing is not null)
        {
            node.Properties.Remove(existing);
        }



        throw new NotImplementedException();
    }
    public IOGraphPropertyDescriptor HasProperty(Name name)
    {
        if (node.Properties.TryGet(name, out var existing) && existing is not null)
        {
            return new OGraphPropertyDescriptor(existing);
        }

        var property = new OGraphProperty()
        {
            Name = name
        };

        node.Properties.Add(property);

        return new OGraphPropertyDescriptor(property);
    }
    public IOGraphNodeDescriptor<T> Ignore<TProperty>(Expression<Func<T, TProperty>> expression)
    {
        ValidateMemberExpression(expression, out var memberInfo);

        if (node.Properties.TryGet(memberInfo.Name, out var property) && property is not null)
        {
            node.Properties.Remove(property);
        }

        return this;
    }
    public IOGraphPropertyDescriptor<TProperty> HasProperty<TProperty>(Expression<Func<T, TProperty>> expression)
    {
        ValidateMemberExpression(expression, out var memberInfo);

        if (node.Properties.TryGet(memberInfo.Name, out var existing) && existing is OGraphProperty cast)
        {
            return new OGraphPropertyDescriptor<TProperty>(cast);
        }

        var method = expression.Compile();

        var property = new OGraphProperty()
        {
            Name = memberInfo.Name,
            Resolver = new OGraphPropertyResolverDefault<TProperty>(context =>
            {
                var parent = context.GetParent<T>();

                if (parent is null)
                {
                    return default;
                }

                return ValueTask.FromResult(method.Invoke(parent));
            })
        };

        node.Properties.Add(property);

        return new OGraphPropertyDescriptor<TProperty>(property);
    }
    public IOGraphEdgeDescriptor<TProperty> HasEdge<TProperty>(Expression<Func<T, IEnumerable<TProperty>>> expression)
    {
        throw new NotImplementedException();
    }





    private void ValidateMemberExpression(LambdaExpression expression, out MemberInfo memberInfo)
    {
        // Check that expression is not null
        if (expression is null)
        {
            throw new ArgumentNullException(nameof(expression));
        }
        // Check that expression is a Property Expression
        if (expression.Body is not MemberExpression member)
        {
            throw new InvalidOperationException($"'{expression}' must be a member expression");
        }
        if (member.Member.DeclaringType is null)
        {
            throw new Exception();
        }
        // Check that the Property is of type TProperty
        if (member.Member.DeclaringType.IsAssignableTo(typeof(T)))
        {
            throw new InvalidOperationException($"'{expression}' must be a member of {typeof(T).Name}");
        }

        memberInfo = member.Member;
    }
    private void OnInitialize(IOGraphPropertyCollection collection, Type type)
    {
        var properties = type.GetProperties().Where(x => x.CanWrite && x.CanRead);

        foreach (var property in properties)
        {
            // Check if Primitive Types
            if (property.PropertyType.IsValueType(out var valueType))
            {
                if (valueType == typeof(DateTime))
                {
                    collection.Add(new OGraphProperty()
                    {
                        Name = property.Name,
                        Resolver = GetResolver(type, property.PropertyType),
                        Type = new StringType()
                    });
                }
            }
            if (property.PropertyType.IsStringType())
            {
                collection.Add(new OGraphProperty()
                {
                    Name        = property.Name,
                    Resolver    = GetResolver(type, property.PropertyType),
                    Type        = new StringType()
                });
            }
            if (property.PropertyType.IsEnumerableType(out var enumerableType))
            {

            }
            if (property.PropertyType.IsComplexType())
            {
                var complexProperties = new OGraphPropertyCollection();

                OnInitialize(complexProperties, property.PropertyType);

                collection.Add(new OGraphProperty()
                {
                    Name        = property.Name,
                    Resolver    = GetResolver(type, property.PropertyType),
                    Type        = new ComplexType()
                    {
                        Properties  = complexProperties,
                        TypeName    = property.PropertyType.Name,
                        RuntimeType = property.PropertyType
                    }
                });
            }
        }

        IOGraphPropertyResolver GetResolver(Type memberDeclaringType, MemberInfo memberInfo)
        {
            var parameter = Expression.Parameter(memberDeclaringType);
            var member = Expression.PropertyOrField(parameter, memberInfo.Name);
            var lambda = Expression.Lambda(member, parameter);
            var method = lambda.Compile();

            return new OGraphPropertyResolverDefault(context =>
            {
                var parent = context.GetParent<object>();
                var value = method.DynamicInvoke(parent);

                return ValueTask.FromResult<IOGraphPropertyResult>(new OGraphPropertyResult()
                {
                    Data = value
                });
            });
        }
    }
}
