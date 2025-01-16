using System;

namespace Assimalign.OGraph;


/// <summary>
/// 
/// </summary>
public abstract class OGraphException : Exception
{
    public OGraphException() { }
    public OGraphException(string message) : base(message) { }
    public OGraphException(string message, Exception innerException): base(message, innerException) { }

    public OGraphErrorCode Code { get; set; }
}