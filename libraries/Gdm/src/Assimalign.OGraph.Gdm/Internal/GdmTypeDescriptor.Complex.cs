using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm.Internal;

internal class GdmComplexTypeDescriptor : IOGraphGdmComplexTypeDescriptor
{
    private readonly GdmComplexType complexType;

    public GdmComplexTypeDescriptor(GdmComplexType complexType)
    {
        this.complexType = complexType;
    }


    public IOGraphGdmComplexTypeDescriptor HasLabel(Label label)
    {
        complexType.Label = label;
        return this;
    }

    public IOGraphGdmPropertyDescriptor HasProperty(Label name)
    {
        throw new NotImplementedException();
    }

    public IOGraphGdmComplexTypeDescriptor HasRuntimeType(Type type)
    {
        throw new NotImplementedException();
    }

    public IOGraphGdmComplexTypeDescriptor HasRuntimeType<T>() where T : class, new()
    {
        throw new NotImplementedException();
    }

    public IOGraphGdmComplexTypeDescriptor Ignore(Label name)
    {
        throw new NotImplementedException();
    }
}
