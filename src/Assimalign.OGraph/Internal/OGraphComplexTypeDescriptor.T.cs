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
        throw new NotImplementedException();
    }

    public IOGraphPropertyDescriptor<TProperty> HasProperty<TProperty>(Expression<Func<T, TProperty>> expression)
    {
        throw new NotImplementedException();
    }

    public IOGraphComplexTypeDescriptor<T> Ignore(Name name)
    {
        throw new NotImplementedException();
    }

    public IOGraphComplexTypeDescriptor<T> Ignore<TProperty>(Expression<Func<T, TProperty>> expression)
    {
        throw new NotImplementedException();
    }
}
