namespace Assimalign.OGraph.Gdm.Internal;

internal class GdmValidatorError
{
    public OGraphGdmErrorCode Code { get; set; }
    public string? Message { get; set; }
    public string? Source { get; set; }
}