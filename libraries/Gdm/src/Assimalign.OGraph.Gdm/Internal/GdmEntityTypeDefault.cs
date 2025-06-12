using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;

namespace Assimalign.OGraph.Gdm.Internal;

using Elements;

internal class GdmEntityTypeDefault<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] T> : GdmEntityType<T>
{
    private readonly Action<GdmEntityTypeDescriptor<T>> _configure;

    public GdmEntityTypeDefault(GdmGraph graph, Action<GdmEntityTypeDescriptor<T>> configure) : base(graph)
    {
        _configure = configure;
    }

    protected override void Configure(GdmEntityTypeDescriptor<T> descriptor)
    {
        _configure.Invoke(descriptor);
    }
}