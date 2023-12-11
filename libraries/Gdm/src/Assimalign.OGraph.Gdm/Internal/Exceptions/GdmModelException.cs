namespace Assimalign.OGraph.Gdm.Internal;

internal class GdmModelException : GdmException
{
    public GdmModelException(GdmValidatorError error) : base(error.Message)
    {
        ErrorCode = error.Code;
        Source = error.Source;
    }

    public GdmModelException(GdmErrorCode errorCode, string? message) 
        : base(message)
    {
        ErrorCode = errorCode;
    }
    public GdmModelException(GdmErrorCode errorCode, string source, string? message) 
        : base(message)
    {
        ErrorCode = errorCode;
        Source = source;
    }
    public override string? Source { get; set; }
    public override GdmErrorCode ErrorCode { get;  }
}