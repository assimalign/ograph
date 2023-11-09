namespace Assimalign.OGraph.Gdm.Internal;

internal class GdmSerializationException : OGraphGdmException
{
    public GdmSerializationException(OGraphGdmErrorCode errorCode, string? message) 
        : base(message)
    {
        ErrorCode = errorCode;
    }
    public GdmSerializationException(OGraphGdmErrorCode errorCode, string source, string? message) 
        : base(message)
    {
        ErrorCode = errorCode;
        Source = source;
    }

    public override string? Source { get; set; }
    public override OGraphGdmErrorCode ErrorCode { get; }
}
