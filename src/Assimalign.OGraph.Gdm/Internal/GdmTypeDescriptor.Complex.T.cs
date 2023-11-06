using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm.Internal;

internal class GdmComplexTypeDescriptor<T> : IOGraphGdmComplexTypeDescriptor<T> 
    where T : class, new()
{
    private readonly GdmComplexType<T> complexType;

    public GdmComplexTypeDescriptor(GdmComplexType<T> complexType)
    {
        this.complexType = complexType;
    }

    public IOGraphGdmComplexTypeDescriptor<T> HasName(Label label)
    {
        throw new NotImplementedException();
    }

    public IOGraphGdmPropertyDescriptor HasProperty(Label name)
    {
        throw new NotImplementedException();
    }

    public IOGraphGdmPropertyDescriptor<TProperty> HasProperty<TProperty>(Expression<Func<T, TProperty>> expression)
    {
        throw new NotImplementedException();
    }

    public IOGraphGdmComplexTypeDescriptor<T> Ignore(Label name)
    {
        throw new NotImplementedException();
    }

    public IOGraphGdmComplexTypeDescriptor<T> Ignore<TProperty>(Expression<Func<T, TProperty>> expression)
    {
        throw new NotImplementedException();
    }
}
