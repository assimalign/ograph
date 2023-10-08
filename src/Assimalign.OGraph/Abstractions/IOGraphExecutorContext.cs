using System;

namespace Assimalign.OGraph;

/// <summary>
/// 
/// </summary>
public interface IOGraphExecutorContext
{
    /// <summary>
    /// 
    /// </summary>
    public IOGraphExecutorRequest Request { get; }
    /// <summary>
    /// 
    /// </summary>
    public IOGraphExecutorResponse Response { get; }
}
