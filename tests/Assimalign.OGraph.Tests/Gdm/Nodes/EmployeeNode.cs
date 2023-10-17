using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm;

internal class EmployeeNode : OGraphNode<EmployeeType>
{
    protected override void Configure(IOGraphNodeDescriptor descriptor)
    {
        descriptor.AddEdge("addresses:primary")
            .UseResolver((context, cancellationToken) =>
            {
                return default;

            });

        descriptor.AddEdge("addresses")
            .UseTargetNode<EmployeeAddressNode>()
            .UseResolver((context, cancellationToken) =>
            {
                return default;
            });

        descriptor.AddQuery("GetUserById");

        descriptor.AddQuery("GetUsers");
    }
}
