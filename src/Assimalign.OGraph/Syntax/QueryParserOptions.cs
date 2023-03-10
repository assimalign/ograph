using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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


    internal IEnumerable<QueryAnalyzer> Analyzers => this.analyzers;



    public void AddAnalyzer(QueryAnalyzer analyzer)
    {
        if (analyzer is null)
        {
            throw new ArgumentNullException(nameof(analyzer));
        }

        analyzers.Add(analyzer);
    }
    public void AddAnalyzer<TAnalyzer>() where TAnalyzer : QueryAnalyzer, new()
    {
        AddAnalyzer(new TAnalyzer());
    }
}
