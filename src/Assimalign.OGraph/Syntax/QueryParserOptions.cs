using System;
using System.Text;
using System.Collections.Generic;

namespace Assimalign.OGraph.Syntax;

using Assimalign.OGraph.Syntax.Analyzer;

public sealed class QueryParserOptions
{
    private readonly IList<QueryAnalyzer> analyzers;

    public QueryParserOptions()
    {
        this.analyzers = new List<QueryAnalyzer>();
    }

    /// <summary>
    /// Throws an exception when there is a Diagnostic Error. The default is 'false'.
    /// </summary>
    public bool ThrowExceptionOnDiagnosticError { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public Encoding Encoding { get; set; } = Encoding.UTF8;
    /// <summary>
    /// The allowed maximum depth of the query. The default is '5'.
    /// </summary>
    public int MaxEdgeDepth { get; set; } = 5;
    /// <summary>
    /// 
    /// </summary>
    public string? StartingVertexName { get; set; }

    internal IEnumerable<QueryAnalyzer> Analyzers => this.analyzers;


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

        analyzers.Add(analyzer);
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
}
