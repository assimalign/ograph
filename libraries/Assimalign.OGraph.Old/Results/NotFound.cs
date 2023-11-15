using System;

namespace Assimalign.OGraph.Results;

public sealed class NotFound : IOGraphError
{
    public NotFound(string message)
    {
        Message = message;
    }

    public NotFound(string code, string message)
    {
        Code = code;
        Message = message;
    }

    /// <inheritdoc />
    public string Code { get; } = "NotFound";

    /// <inheritdoc />
    public string Message { get; }

    /// <inheritdoc />
    public IOGraphErrorDetailsCollection? Details => throw new NotImplementedException();
}
