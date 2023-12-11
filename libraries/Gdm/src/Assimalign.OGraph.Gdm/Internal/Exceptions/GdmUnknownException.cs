namespace Assimalign.OGraph.Gdm.Internal;

using Properties;

internal class GdmUnknownException : GdmException
{
    public GdmUnknownException() : base(Resources.GDM0000)
    {
        ErrorCode = GdmErrorCode.GDM0000;
    }

    public override GdmErrorCode ErrorCode { get; }
}
