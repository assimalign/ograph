namespace Assimalign.OGraph.Gdm.Internal;

internal class GdmSerializationException : GdmException
{
    public GdmSerializationException(GdmErrorCode errorCode, string? message) 
        : base(message)
    {
        ErrorCode = errorCode;
    }
    public GdmSerializationException(GdmErrorCode errorCode, string source, string? message) 
        : base(message)
    {
        ErrorCode = errorCode;
        Source = source;
    }

    public override string? Source { get; set; }
    public override GdmErrorCode ErrorCode { get; }
}
