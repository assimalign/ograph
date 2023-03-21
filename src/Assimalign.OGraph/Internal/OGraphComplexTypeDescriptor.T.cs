using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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
        if (memberExpression.Type.DeclaringType is null || !memberExpression.Type.DeclaringType.IsAssignableTo(typeof(T)))
        {
            throw new Exception();
        }

        return new OGraphPropertyDescriptor<TProperty>(new OGraphProperty()
        {
            Name = memberExpression.Member.Name
        });
    }

    public IOGraphComplexTypeDescriptor<T> Ignore(Name name)
    {
        if (complexType.Properties.TryGet(name, out var property))
        {
            complexType.Properties.Remove(property);
        }

        return this;
    }

    public IOGraphComplexTypeDescriptor<T> Ignore<TProperty>(Expression<Func<T, TProperty>> expression)
    {
        var memberExpression = (MemberExpression)expression.Body;

        if (complexType.Properties.TryGet(memberExpression.Member.Name, out var property))
        {
            complexType.Properties.Remove(property);
        }

        return this;
    }
}
