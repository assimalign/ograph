using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Assimalign.OGraph.Gdm.Internal;

using Elements;

internal class GdmComplexTypeDefault<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] T> : GdmComplexType<T>
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
