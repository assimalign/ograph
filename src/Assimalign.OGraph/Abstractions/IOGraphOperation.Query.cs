namespace Assimalign.OGraph;

public interface IOGraphQueryOperation : IOGraphOperation
{
    /// <summary>
    /// Gets the query options to be used for the query provider.
    /// </summary>
    OGraphQueryOptions QueryOptions { get; }
    /// <summary>
    /// Gets the Query provider.
    /// </summary>
    IOGraphQueryProvider QueryProvider { get; }
}