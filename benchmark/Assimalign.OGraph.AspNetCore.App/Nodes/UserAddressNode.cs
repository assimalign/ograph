namespace Assimalign.OGraph;

public class UserAddressNode : OGraphNode<UserAddressType>
{
    protected override void Configure(IOGraphNodeDescriptor descriptor)
    {
        descriptor.AddEdge("hasUser")
            .UseTargetNode<UserNode>()
            .UseResolver(context =>
            {


                throw new NotImplementedException();
            });
    }   
}
