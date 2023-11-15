using System;

namespace Assimalign.OGraph;

/// <summary>
/// A fluent builder for creating a <see cref="IOGraph"/> model.
/// </summary>
public interface IOGraphBuilder
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="configure"></param>
    /// <returns></returns>
    IOGraphBuilder ConfigureOptions(Action<OGraphOptions> configure);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="configure"></param>
    /// <returns></returns>
    IOGraphBuilder ConfigureModel(Action<IOGraphModelDescriptor> configure);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="configure"></param>
    /// <returns></returns>
    IOGraphBuilder ConfigureApplication(Action<IOGraphApplication> configure);
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    IOGraph Build();
}