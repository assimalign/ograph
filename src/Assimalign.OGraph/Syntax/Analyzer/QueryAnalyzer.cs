using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Assimalign.OGraph.Syntax.Analyzer;

/// <summary>
/// 
/// </summary>
public abstract class QueryAnalyzer
{

    /// <summary>
    /// 
    /// </summary>
    /// <param name="query"></param>
    /// <param name="diagnostics"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public abstract Task AnalyzeAsync(QueryDocument document, CancellationToken cancellationToken);
}
