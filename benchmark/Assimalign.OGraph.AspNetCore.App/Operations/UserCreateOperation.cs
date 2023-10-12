namespace Assimalign.OGraph;

public class UserCreateOperation : OGraphCommandOperation<UserNode>
{
    protected override void Configure(IOGraphCommandOperationDescriptor descriptor)
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