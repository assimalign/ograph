namespace Assimalign.OGraph;

public class UserCreateOperation : OGraphOperation<UserNode>
{
    protected override void Configure(IOGraphOperationDescriptor descriptor)
    {
        descriptor.UseName(nameof(UserCreateOperation))
            .UseRoute("/users")
            .UseResolver(context =>
            {

                return new User()
                {

                };
                throw new NotImplementedException();
            });
    }
}