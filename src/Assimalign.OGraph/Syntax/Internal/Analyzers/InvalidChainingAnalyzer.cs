using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Syntax.Internal;

using Assimalign.OGraph.Syntax.Analyzer;

internal class InvalidChainingAnalyzer : QueryAnalyzer
{
    public override Task AnalyzeAsync(QueryDocument document, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
