namespace Assimalign.OGraph;

public class UserType : ComplexType<User>
{
    protected override void Configure(IOGraphComplexTypeDescriptor<User> descriptor)
    {
        //descriptor.HasProperty("fullName")
        //    .UseResolver(context =>
        //    {
        //        var parent = context.GetParent<User>();

        //        return $"{parent.Details.LastName}, {parent.Details.FirstName}";
        //    });
    }
}