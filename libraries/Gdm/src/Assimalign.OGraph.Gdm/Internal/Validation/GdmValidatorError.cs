namespace Assimalign.OGraph.Gdm.Internal;

internal class GdmValidatorError
{
    public GdmErrorCode Code { get; set; }
    public string? Message { get; set; }
    public string? Source { get; set; }
}