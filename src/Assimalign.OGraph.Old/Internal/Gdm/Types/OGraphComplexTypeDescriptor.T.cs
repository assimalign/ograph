using System;
using System.Linq.Expressions;

namespace Assimalign.OGraph.Internal;

internal class OGraphComplexTypeDescriptor<T> : IOGraphComplexTypeDescriptor<T>
{
    private readonly ComplexType type;

    public OGraphComplexTypeDescriptor(ComplexType complexTyp)
    {
        if (complexTyp is null)
        {
            throw new ArgumentNullException(nameof(complexTyp));
        }
        this.type = complexTyp;
    }

    public IOGraphPropertyDescriptor HasProperty(Label name)
    {
        var property = new Property()
        {
            Name = name
        };
        if (type.Properties.TryGetProperty(name, out var prop) && prop is Property prop1)
        {
            property = prop1;
        }
        type.Properties.Add(property);
        return new OGraphPropertyDescriptor(property);
    }

    public IOGraphPropertyDescriptor<TProperty> HasProperty<TProperty>(Expression<Func<T, TProperty>> expression)
    {
        if (expression is null)
        {
            throw new ArgumentNullException(nameof(expression));
        }
        if (expression.Body is not MemberExpression memberExpression)
        {
            throw new Exception();
        }
        if (memberExpression.Member.DeclaringType is null || !memberExpression.Member.DeclaringType.IsAssignableTo(typeof(T)))
        {
            throw new Exception();
        }

        var propertyName = memberExpression.Member.Name;

        if (type.Properties.TryGetProperty(propertyName, out var property))
        {
            if (property is not Property prop)
            {
                throw new Exception("Something Happened");
            }
            return new PropertyDescriptor<TProperty>(prop);
        }

        return new PropertyDescriptor<TProperty>(new Property()
        {
            Name = memberExpression.Member.Name
        });
    }
    public IOGraphComplexTypeDescriptor<T> Ignore(Label name)
    {
        if (type.Properties.TryGetProperty(name, out var property))
        {
            type.Properties.Remove(property);
        }

        return this;
    }
    public IOGraphComplexTypeDescriptor<T> Ignore<TProperty>(Expression<Func<T, TProperty>> expression)
    {
        var memberExpression = (MemberExpression)expression.Body;

        if (type.Properties.TryGetProperty(memberExpression.Member.Name, out var property))
        {
            type.Properties.Remove(property);
        }

        return this;
    }
}
