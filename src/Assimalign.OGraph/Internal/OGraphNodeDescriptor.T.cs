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

        OnInitialize();
    }


    private void OnInitialize()
    {
        foreach (var property in typeof(T).GetProperties().Where(x => x.CanWrite && x.CanRead))
        {
            // Check if Primitive Types
            if (property.PropertyType.IsValueType(out var valueType))
            {
                if (valueType == typeof(DateTime))
                {
                    node.Properties.Add(new OGraphProperty()
                    {
                        Name = property.Name,
                        Resolver = GetResolver<DateTime>(property)
                    });
                }
            }
            if (property.PropertyType.IsStringType())
            {
                node.Properties.Add(new OGraphProperty()
                {
                    Name = property.Name,
                    Resolver = GetResolver<String>(property)
                });
            }

            //if (property.PropertyType.IsValueType() || property.PropertyType.IsStringType() || property.PropertyType.IsEnum)
            //{
            //    var resolver = OGraphEdgeResolver()
            //    node.Properties.Add(new OGraphProperty()
            //    {
            //        Name = property.Name,
            //        Resolver
            //    });
            //}
            if (property.PropertyType.IsEnumerableType(out var enumerableType))
            {

            }
            if (property.PropertyType.IsComplexType())
            {

            }
        }

        IOGraphPropertyResolver GetResolver<TProperty>(MemberInfo memberInfo)
        {
            var parameter = Expression.Parameter(typeof(T));
            var member = Expression.PropertyOrField(parameter, memberInfo.Name);
            var lambda = Expression.Lambda<Func<T, TProperty>>(member, parameter);
            var method = lambda.Compile();

            return new OGraphPropertyResolverDefault<TProperty>(context =>
            {
                var parent = context.GetParent<T>();

                return ValueTask.FromResult(method.Invoke(parent));
            });
        }
    }

    public IOGraphNodeDescriptor<T> HasLabel(Label label)
    {
        throw new NotImplementedException();
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

        // TODO: Need to remove property from initialized collection 



        throw new NotImplementedException();
    }

    public IOGraphNodeDescriptor<T> HasKey<TProperty>(Expression<Func<T, TProperty>> expression)
    {
        ValidateMemberExpression(expression, out var memberInfo);



        throw new NotImplementedException();
    }

    public IOGraphPropertyDescriptor HasProperty(Name name)
    {
        if (node.Properties.Any(property => property.Name == name))
        {

        }
        throw new NotImplementedException();
    }

    public IOGraphPropertyDescriptor<TProperty> HasProperty<TProperty>(Expression<Func<T, TProperty>> expression)
    {
        ValidateMemberExpression(expression, out var memberInfo);

        return new OGraphPropertyDescriptor<TProperty>(new OGraphProperty()
        {
            Name = memberInfo.Name
        });
    }

    public IOGraphNodeDescriptor<T> Ignore<TProperty>(Expression<Func<T, TProperty>> expression)
    {
        ValidateMemberExpression(expression, out var memberInfo);


        throw new NotImplementedException();
    }



    public IOGraphEdgeDescriptor<TProperty> HasEdge<TProperty>(Expression<Func<T, IEnumerable<TProperty>>> expression)
    {
        throw new NotImplementedException();
    }





    private void ValidateMemberExpression(Expression expression, out MemberInfo memberInfo)
    {
        // Check that expression is not null
        if (expression is null)
        {
            throw new ArgumentNullException(nameof(expression));
        }
        // Check that expression is a Property Expression
        if (expression is not LambdaExpression lambda || lambda.Body is not MemberExpression member)
        {
            throw new InvalidOperationException($"'{expression}' must be a member expression");
        }
        // Check that the Property is of type T
        if (member.Member.DeclaringType.IsAssignableTo(typeof(T)))
        {
            throw new InvalidOperationException($"'{expression}' must be a member of {typeof(T).Name}");
        }

        memberInfo = member.Member;
    }
}
