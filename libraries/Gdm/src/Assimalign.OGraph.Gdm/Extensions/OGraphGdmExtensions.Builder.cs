using System;

namespace Assimalign.OGraph.Gdm;

using Assimalign.OGraph.Gdm.Elements;

public static class OGraphGdmBuilderExtensions
{
    /// <summary>
    /// 
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
        return builder.AddGraph(GdmGraph.Create(label, configure));
    }
}