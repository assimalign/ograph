using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Syntax.Internal;

internal class InvalidPageConstantsAnalyzer : QueryAnalyzer
{
    public override Task AnalyzeAsync(QueryDocument document, CancellationToken cancellationToken = default)
    {
        return Task.Run(() =>
        {
            if (document.Root is not null)
            {
                foreach (var node in document.Root.GetNodesOfType<PageNode>())
                {
                    //node.Take.
                }
            }
        });
    }
}
