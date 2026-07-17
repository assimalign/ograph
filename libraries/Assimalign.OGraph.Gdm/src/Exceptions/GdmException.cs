using System;
using System.Diagnostics;

namespace Assimalign.OGraph.Gdm;

/// <summary>
/// 
/// </summary>
[DebuggerDisplay("{ErrorCode} - {Message}. Error occurred at: {Source}")]
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
    public abstract GdmErrorCode ErrorCode { get; }

    /// <summary>
    /// 
    /// </summary>
    public override string? Source { get; set; } = "Unknown";
}
