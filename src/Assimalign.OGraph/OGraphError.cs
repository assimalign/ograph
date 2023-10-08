namespace Assimalign.OGraph;

/// <inheritdoc />
public sealed class OGraphError : IOGraphError
{
    public OGraphError()
    {
        this.Details = new OGraphErrorDetailsCollection();
    }

    /// <inheritdoc />
    public string? Code { get; set; }

    /// <inheritdoc />
    public string? Message { get; set; }

    /// <inheritdoc />
    public IOGraphErrorDetailsCollection Details { get; }
}