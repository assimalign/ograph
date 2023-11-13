namespace Assimalign.OGraph;

public class UserVertex : OGraphVertex<User>
{
    protected override void Configure(IOGraphVertexDescriptor<User> descriptor)
    {
        descriptor.HasLabel("user");

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