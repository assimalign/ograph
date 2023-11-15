using System;
using System.Diagnostics;

namespace Assimalign.OGraph.Gdm;

/// <summary>
/// 
/// </summary>
[DebuggerDisplay("{ErrorCode} - {Message}. Error occurred at or on: {Source}")]
public abstract class OGraphGdmException : Exception
{
    protected OGraphGdmException(string? message) 
        : base(message)
    {
    }
    protected OGraphGdmException(string? message, Exception? innerException) 
        : base(message, innerException)
    {
    }

    /// <summary>
    /// 
    /// </summary>
    public virtual OGraphGdmErrorCode ErrorCode { get; }
}
