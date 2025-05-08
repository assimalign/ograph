using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm.Internal;

using Elements;

internal class GdmComplexTypeDefault<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] T> : GdmComplexType<T>
    where T : class, new()
{
    private readonly Action<GdmComplexTypeDescriptor<T>> _configure;

    public GdmComplexTypeDefault(GdmGraph graph, Action<GdmComplexTypeDescriptor<T>> configure) : base(graph)
    {
        _configure = configure;
    }

    protected override void Configure(GdmComplexTypeDescriptor<T> descriptor)
    {
        _configure.Invoke(descriptor);
    }
}
