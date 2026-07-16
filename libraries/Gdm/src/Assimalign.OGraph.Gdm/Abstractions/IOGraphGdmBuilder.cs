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
    /// <param name="action"></param>
    /// <returns></returns>
    IOGraphGdmBuilder BeforeBuild(Action<IOGraphGdm> action);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="action"></param>
    /// <returns></returns>
    IOGraphGdmBuilder AfterBuild(Action<IOGraphGdm> action);

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

    //IOGraphGdmBuilder AddRemoteGraph()

    /// <summary>
    ///
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    IOGraphGdmBuilder AddMeta(string key, string value);

    /// <summary>
    ///
    /// </summary>
    /// <returns></returns>
    IOGraphGdm Build();
}