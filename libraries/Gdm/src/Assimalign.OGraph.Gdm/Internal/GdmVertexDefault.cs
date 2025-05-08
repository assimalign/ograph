using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm.Internal;

using Elements;

internal class GdmVertexDefault<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] T> : GdmVertex<T>
    where T : class, new()
{
    private readonly Action<GdmVertexDescriptor<T>> _configure;
    public GdmVertexDefault(GdmGraph graph, Action<GdmVertexDescriptor<T>> configure) : base(graph)
    {
        _configure = configure;
    }

    protected override void Configure(GdmVertexDescriptor<T> descriptor)
    {
        _configure.Invoke(descriptor);
    }
}
