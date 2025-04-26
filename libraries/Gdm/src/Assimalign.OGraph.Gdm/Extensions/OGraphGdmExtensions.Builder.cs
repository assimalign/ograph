using System;

namespace Assimalign.OGraph.Gdm;

using Elements;
using Internal;

public static class OGraphGdmBuilderExtensions
{
    /// <summary>
    /// Adds a graph to
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="label"></param>
    /// <param name="configure"></param>
    /// <returns></returns>
    public static IOGraphGdmBuilder AddGraph(
        this IOGraphGdmBuilder builder, 
        GdmLabel label,
        Action<IOGraphGdmGraphDescriptor> configure)
    {
        ThrowHelper.ThrowIfNull(builder, nameof(builder));
        ThrowHelper.ThrowIfNull(configure, nameof(configure));

        return builder.AddGraph(GdmGraph.Create(label, configure));
    }
}