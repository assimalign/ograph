using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm.Internal;

internal class GdmComplexTypeDescriptor<T> : IOGraphGdmComplexTypeDescriptor<T> where T : class, new()
{
    private readonly ComplexType<T> complexType;

    public GdmComplexTypeDescriptor(ComplexType<T> complexType)
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

    public IOGraphGdmPropertyDescriptor<TProperty> HasProperty<TProperty>(System.Linq.Expressions.Expression<Func<T, TProperty>> expression)
    {
        throw new NotImplementedException();
    }

    public IOGraphGdmComplexTypeDescriptor<T> Ignore(Label name)
    {
        throw new NotImplementedException();
    }

    public IOGraphGdmComplexTypeDescriptor<T> Ignore<TProperty>(System.Linq.Expressions.Expression<Func<T, TProperty>> expression)
    {
        throw new NotImplementedException();
    }
}
