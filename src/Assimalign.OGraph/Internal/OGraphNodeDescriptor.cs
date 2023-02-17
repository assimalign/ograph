using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Internal;

internal class OGraphNodeDescriptor<T> : IOGraphNodeDescriptor<T>
{

    public OGraphNodeDescriptor(IOGraphNode node)
    {
        this.Node = node;
        OnInitialize();
    }


    private void OnInitialize()
    {
        foreach (var property in typeof(T).GetProperties().Where(x => x.CanWrite && x.CanRead))
        {
            if (property.PropertyType.IsValueType || property.PropertyType.IsEnum)
            {
                var nodeProperty = new OGraphNodeProperty()
                {
                    IsFilterable = true,
                    IsComputed = true,
                    PropertyName = property.Name,

                };

                Node.Properties.Add(nodeProperty);
            }
            if (property.PropertyType == typeof(string))
            {
                var nodeProperty = new OGraphNodeProperty()
                {
                    IsFilterable = true,
                    IsComputed = true,
                    PropertyName = property.Name,

                };

                Node.Properties.Add(nodeProperty);
            }
        }
    }


    private void Test()
    {
        var parameterExpression = Expression.Parameter(typeof(T));
        var memberExpression = Expression.PropertyOrField(parameterExpression, "");
        var lambdaExpression = Expression.Lambda(memberExpression, parameterExpression);
    }



    public IOGraphNode Node { get; init; }



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

    public IOGraphNodePropertyDescriptor HasProperty(Name name)
    {
        throw new NotImplementedException();
    }

    public IOGraphNodePropertyDescriptor<TProperty> HasProperty<TProperty>(Expression<Func<T, TProperty>> expression)
    {
        if (expression is null)
        {
            throw new ArgumentNullException(nameof(expression));
        }
        if (expression.Body is not MemberExpression member)
        {
            throw new InvalidOperationException($"'{expression}' must be a member expression");
        }
        var property = new OGraphNodeProperty()
        {
            PropertyName = member.Member.Name
        };
        return new OGraphNodePropertyDescriptor<TProperty>()
        {
            Property = property
        };
    }

    public IOGraphNodeDescriptor<T> Ignore<TProperty>(Expression<Func<T, TProperty>> expression)
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
        // Check that expression is a Member Expression
        if (expression is not LambdaExpression lambda || lambda.Body is not MemberExpression member)
        {
            throw new InvalidOperationException($"'{expression}' must be a member expression");
        }
        // Check that the Member is of type T
        if (member.Member.DeclaringType.IsAssignableTo(typeof(T)))
        {
            throw new InvalidOperationException($"'{expression}' must be a member of {typeof(T).Name}");
        }

        memberInfo = member.Member;
    }
}
