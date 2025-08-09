using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Syntax.Internal;

internal class InvalidPageConstantsAnalyzer : QueryAnalyzer
{
    public override Task AnalyzeAsync(QueryAnalyzerContext context, CancellationToken cancellationToken = default)
    {
        return Task.Run(() =>
        {
            if (context.Document.Root is not null)
            {
                foreach (var node in context.Document.Root.GetNodesOfType<PageNode>())
                {
                    //node.Take.
                }
            }
        });
    }
}
