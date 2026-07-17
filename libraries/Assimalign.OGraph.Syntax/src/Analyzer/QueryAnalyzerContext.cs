using System;
using System.Collections.Generic;
using System.Text;

namespace Assimalign.OGraph.Syntax;

/// <summary>
/// 
/// </summary>
public sealed class QueryAnalyzerContext
{
    internal QueryAnalyzerContext(QueryDocument document)
    {
        Document = document;
    }

    /// <summary>
    /// The parsed query document.
    /// </summary>
    public QueryDocument Document { get; }
}
