using System;

namespace Assimalign.OGraph;

/// <summary>
/// 
/// </summary>
public interface IOGraphExecutorContext
{
    /// <summary>
    /// The incoming HTTP request
    /// </summary>
    public IOGraphExecutorRequest Request { get; }
    /// <summary>
    /// The outgoing HTTP response.
    /// </summary>
    public IOGraphExecutorResponse Response { get; }
}
