using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm.Internal;

internal class GdmPropertyDescriptor : IOGraphGdmPropertyDescriptor
{
    private readonly GdmProperty property;

    public GdmPropertyDescriptor(GdmProperty property)
    {
        this.property = property;
    }

    public GdmBuilderContext Context { get; init; } = default!;

    public IOGraphGdmPropertyDescriptor UseType<TType>() where TType : IOGraphGdmType, new()
    {
        throw new NotImplementedException();
    }
}
