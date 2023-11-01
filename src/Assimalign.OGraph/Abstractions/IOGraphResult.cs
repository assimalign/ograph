namespace Assimalign.OGraph;

/// <summary>
/// The base result used in all resolvers.
/// </summary>
public interface IOGraphResult
{
    /// <summary>
    /// The status code of the result set.
    /// </summary>
    StatusCode StatusCode { get; }
}