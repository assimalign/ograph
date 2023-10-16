using System;
using System.Linq.Expressions;

namespace Assimalign.OGraph.Internal;

internal class OGraphComplexTypeDescriptor<T> : IOGraphComplexTypeDescriptor<T>
{
    private readonly IOGraphComplexType complexType;

    public OGraphComplexTypeDescriptor(IOGraphComplexType complexTyp)
    {
        if (complexTyp is null)
        {
            throw new ArgumentNullException(nameof(complexTyp));
        }
        this.complexType = complexTyp;
    }

    public IOGraphPropertyDescriptor HasProperty(Name name)
    {
        return new OGraphPropertyDescriptor(new OGraphProperty()
        {
            Name = name,
        });
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
        if (complexType.Properties.TryGetProperty(memberExpression.Member.Name, out var property))
        {
            if (property is not OGraphProperty prop)
            {
                throw new Exception("Something Happened");
            }
            return new OGraphPropertyDescriptor<TProperty>(prop);
        }

        return new OGraphPropertyDescriptor<TProperty>(new OGraphProperty()
        {
            Name = memberExpression.Member.Name
        });
    }

    public IOGraphComplexTypeDescriptor<T> Ignore(Name name)
    {
        if (complexType.Properties.TryGetProperty(name, out var property))
        {
            complexType.Properties.Remove(property);
        }

        return this;
    }

    public IOGraphComplexTypeDescriptor<T> Ignore<TProperty>(Expression<Func<T, TProperty>> expression)
    {
        var memberExpression = (MemberExpression)expression.Body;

        if (complexType.Properties.TryGetProperty(memberExpression.Member.Name, out var property))
        {
            complexType.Properties.Remove(property);
        }

        return this;
    }
}
