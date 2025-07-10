using System;
using System.Text;
using System.Collections.Generic;

namespace Assimalign.OGraph.Syntax;

using Assimalign.OGraph.Syntax.Internal;

public sealed class QueryParserOptions
{
    private readonly IList<QueryAnalyzer> _analyzers;

    public QueryParserOptions()
    {
        _analyzers = new List<QueryAnalyzer>()
        {
            new InvalidNodeTypesInVertexAnalyzer()
        };
    }

    /// <summary>
    /// Throws an exception when there is a Diagnostic Error. The default is 'false'.
    /// </summary>
    public bool ThrowExceptionOnDiagnosticError { get; set; }

    /// <summary>
    /// Gets or sets the encoding for the query parser. The default is UTF8.
    /// </summary>
    public Encoding Encoding { get; set; } = Encoding.UTF8;

    /// <summary>
    /// Specify the timeout for query analysis. Default is 5 seconds.
    /// </summary>
    public TimeSpan AnalyzerTimeout { get; set; } = TimeSpan.FromSeconds(5);

    /// <summary>
    /// 
    /// </summary>
    public IReadOnlyCollection<QueryAnalyzer> Analyzers => _analyzers.AsReadOnly();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="analyzer"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public void AddAnalyzer(QueryAnalyzer analyzer)
    {
        if (analyzer is null)
        {
            throw new ArgumentNullException(nameof(analyzer));
        }

        _analyzers.Add(analyzer);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TAnalyzer"></typeparam>
    /// <exception cref="ArgumentNullException"></exception>
    public void AddAnalyzer<TAnalyzer>() where TAnalyzer : QueryAnalyzer, new()
    {
        AddAnalyzer(new TAnalyzer());
    }



    public static QueryParserOptions Default => new QueryParserOptions();
}
