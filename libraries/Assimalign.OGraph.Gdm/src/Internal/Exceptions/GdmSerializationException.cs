using System;

namespace Assimalign.OGraph.Gdm.Internal;

internal class GdmSerializationException : GdmException
{
    public GdmSerializationException(GdmErrorCode errorCode, string? message) 
        : base(message)
    {
        ErrorCode = errorCode;
    }

    public GdmSerializationException(GdmErrorCode errorCode, string? message, Exception innerException)
        : base(message, innerException)
    {
        ErrorCode = errorCode;
    }

    public override GdmErrorCode ErrorCode { get; }
}
