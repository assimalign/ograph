namespace Assimalign.OGraph.Gdm.Internal;

internal class GdmModelException : OGraphGdmException
{
    public GdmModelException(GdmValidatorError error) : base(error.Message)
    {
        ErrorCode = error.Code;
        Source = error.Source;
    }

    public GdmModelException(OGraphGdmErrorCode errorCode, string? message) 
        : base(message)
    {
        ErrorCode = errorCode;
    }
    public GdmModelException(OGraphGdmErrorCode errorCode, string source, string? message) 
        : base(message)
    {
        ErrorCode = errorCode;
        Source = source;
    }
    public override string? Source { get; set; }
    public override OGraphGdmErrorCode ErrorCode { get;  }
}