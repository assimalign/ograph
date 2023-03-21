namespace Assimalign.OGraph;

using Assimalign.OGraph.Syntax;

public sealed class OGraphOptions
{
    /// <summary>
    /// Specify the prefix to be used on all operation routes.
    /// </summary>
    public string? RoutePrefix { get; }
    /// <summary>
    /// 
    /// </summary>
    public QueryParserOptions ParserOptions { get; set; } = new();
}
