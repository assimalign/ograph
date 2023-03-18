using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Internal;

internal class OGraphComplexTypeDescriptor<T> : IOGraphComplexTypeDescriptor<T>
{
    private readonly IOGraphComplexType complexType;

    public OGraphComplexTypeDescriptor(IOGraphComplexType complexTyp)
    {
        this.complexType = complexTyp;
    }

    public IOGraphPropertyDescriptor HasProperty(Name name)
    {
        return default;
    }

    public IOGraphPropertyDescriptor<TProperty> HasProperty<TProperty>(Expression<Func<T, TProperty>> expression)
    {
        return default;
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
