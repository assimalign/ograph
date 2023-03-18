namespace Assimalign.OGraph;

public class UserNode : OGraphNode<UserType>
{
    protected override void Configure(IOGraphNodeDescriptor descriptor)
    {

        descriptor.UseLabel("Users");

        descriptor.AddEdge("companies")
            .UseTargetNode<CompanyNode>()

            .UseResolver(context =>
            {

                throw new NotImplementedException();
            });


        descriptor.AddEdge("hasEmployeeProfile")
            .UseTargetNode<EmployeeNode>()
            .UseResolver(context =>
            {
                throw new NotImplementedException();
            });
    }
}
