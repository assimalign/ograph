using System;
using System.Diagnostics;

namespace Assimalign.OGraph.Gdm;

/// <summary>
/// 
/// </summary>
[DebuggerDisplay("{ErrorCode} - {Message}. Error occurred at or on: {Source}")]
public abstract class GdmException : Exception
{
    protected GdmException(string? message) 
        : base(message)
    {
    }
    protected GdmException(string? message, Exception? innerException) 
        : base(message, innerException)
    {
    }

    /// <summary>
    /// 
    /// </summary>
    public virtual GdmErrorCode ErrorCode { get; }
}
