namespace Assimalign.OGraph;

public class UserVertex : OGraphVertex<User>
{
    protected override void Configure(IOGraphVertexDescriptor<User> descriptor)
    {
        descriptor.HasLabel("user");
        descriptor.HasKey(p => p.UserId);

        descriptor.HasProperty(p => p.Details)
            .UseType(details => // Extending the details type
            {
                details.HasProperty("fullName")
                    .UseMetadata("", "")
                    .UseType<StringType>()
                    .UseMiddleware((context, next) =>
                    {
                        return next(context);
                    })
                    .UseResolver(context => // Creating a resolver for a computed property
                    {
                        var parent = context.GetParent<UserDetails>();

                        return $"{parent.LastName}, {parent.FirstName} {parent.MiddleName}";
                    });
            });

        // Route Representation: /users/{userId}/addresses --> There should be a corresponding operation bound to the addresses vertex
        descriptor.HasEdge("addresses")
            .UseTarget<UserAddressVertex>();

        // Route Representation: /users/{userId}/primaryAddress
        descriptor.HasEdge("primaryAddress")
            .UseTarget<UserAddressVertex>();

        // Route Representation: /users/{userId}/profile
        descriptor.HasEdge("profile")
            .UseTarget<UserProfileVertex>();
    }
}