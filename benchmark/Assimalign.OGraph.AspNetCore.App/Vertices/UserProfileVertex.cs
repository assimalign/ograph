namespace Assimalign.OGraph;

public class UserProfileVertex : OGraphVertex<UserProfile>
{
    protected override void Configure(IOGraphVertexDescriptor<UserProfile> descriptor)
    {
        descriptor.HasLabel("UserProfile");
    }
}
