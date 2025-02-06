using System;

namespace Assimalign.OGraph.Gdm;

/// <summary>
/// 
/// </summary>
public interface IOGraphGdmBuilder
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="graph"></param>
    /// <returns></returns>
    IOGraphGdmBuilder AddGraph(IOGraphGdmGraph graph);


    //IOGraphGdmBuilder AddRemoteGraph()

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    IOGraphGdm Build();
}