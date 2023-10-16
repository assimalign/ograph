using System;


namespace Assimalign.OGraph;

/// <summary>
/// 
/// </summary>
public interface IOGraphExecutorBuilder
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="configure"></param>
    /// <returns></returns>
    IOGraphExecutorBuilder AddGraph(Action<IOGraphBuilder> configure);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="options"></param>
    /// <returns></returns>
    IOGraphExecutorBuilder AddOptions(Action<OGraphOptions> options);
    /// <summary>
    /// Builds the executor.
    /// </summary>
    /// <returns></returns>
    IOGraphExecutor Build();
}