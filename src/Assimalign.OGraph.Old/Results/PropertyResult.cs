namespace Assimalign.OGraph;

public sealed class PropertyResult : IOGraphPropertyResult
{
    public StatusCode StatusCode => 200;
    public object? Value { get; init; }
}
