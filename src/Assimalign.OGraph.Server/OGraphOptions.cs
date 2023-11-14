using System;

namespace Assimalign.OGraph;

using Assimalign.OGraph.Syntax;

/// <summary>
/// 
/// </summary>
public sealed class OGraphOptions
{
    /// <summary>
    /// Specify the prefix to be used on all operation routes.
    /// </summary>
    public string? RoutePrefix { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string DefaultMediaType { get; set; } = OGraphMediaType.Json;
    /// <summary>
    /// Specifies the <see cref="QueryParser"/> options.
    /// </summary>
    public QueryParserOptions ParserOptions { get; set; } = new();
    /// <summary>
    /// 
    /// </summary>
    public IServiceProvider? ServiceProvider { get; set; }
}
