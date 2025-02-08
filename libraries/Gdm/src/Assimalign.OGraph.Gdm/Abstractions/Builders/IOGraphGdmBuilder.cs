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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="graph"></param>
    /// <returns></returns>
    IOGraphGdmBuilder AddGraph(Func<IOGraphGdm, IOGraphGdmGraph> graph);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    IOGraphGdmBuilder AddMeta(string key, string value);


    //IOGraphGdmBuilder AddRemoteGraph()

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    IOGraphGdm Build();
}