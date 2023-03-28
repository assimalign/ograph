using System;

namespace Assimalign.OGraph;

public abstract class OGraphException : Exception
{
    public OGraphException(string message) : base(message)
    {
        
    }

    public OGraphException(string message, Exception innerException) : base(message, innerException)
    {
        
    }


    public abstract OGraphErrorType ErrorType { get; }
}
