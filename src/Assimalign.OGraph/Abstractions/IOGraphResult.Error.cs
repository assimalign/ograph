namespace Assimalign.OGraph;

public interface IOGraphErrorResult : IOGraphResult
{
    IOGraphError Error { get; }
}
