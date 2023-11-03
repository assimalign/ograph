namespace Assimalign.OGraph;

/// <inheritdoc cref="IOGraphErrorDetails" />
public sealed class OGraphErrorDetails : IOGraphErrorDetails
{
    public string? Title { get; set; }
    public string? Message { get; set; }
}
