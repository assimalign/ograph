using System;

namespace Assimalign.OGraph.Gdm;

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
}
