using Assimalign.OGraph.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Execution;

internal class OGraphQueryStrategy
{





    public Task ExecuteAsync()
    {

        return Task.CompletedTask;
    }


    public Task RunProjections(RootQueryNode root)
    {
        if (root.TryGetProjections(out var projections))
        {
            foreach (var projection in projections)
            {
                
            }
        }

        return Task.CompletedTask;
    }
}
