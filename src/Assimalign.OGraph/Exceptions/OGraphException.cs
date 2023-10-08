using System;

namespace Assimalign.OGraph;

/// <summary>
/// 
/// </summary>
public abstract class OGraphException : Exception
{
    public OGraphException(string message) 
        : base(message) { }
    
    public OGraphException(string message, Exception innerException) 
        : base(message, innerException) { }

    /// <summary>
    /// 
    /// </summary>
    public abstract OGraphErrorType ErrorType { get; }
    /// <summary>
    /// The unique error code for the exception.
    /// </summary>
    public abstract OGraphErrorCode ErrorCode { get; }
}
