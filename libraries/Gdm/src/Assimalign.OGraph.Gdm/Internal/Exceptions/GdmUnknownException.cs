namespace Assimalign.OGraph.Gdm.Internal;

using Properties;

internal class GdmUnknownException : OGraphGdmException
{
    public GdmUnknownException() : base(Resources.GDM0000)
    {
        ErrorCode = OGraphGdmErrorCode.GDM0000;
    }

    public override OGraphGdmErrorCode ErrorCode { get; }
}
