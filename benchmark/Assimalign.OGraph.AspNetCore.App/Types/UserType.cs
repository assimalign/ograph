namespace Assimalign.OGraph;

public class UserType : ComplexType<User>
{
    protected override void Configure(IOGraphComplexTypeDescriptor<User> descriptor)
    {
        descriptor.HasProperty(p => p.Details)
            .UseType(details =>
            {
                details.HasProperty("fullName")
                    .UseType<StringType>()
                    .UseMiddleware((context, next) => next(context))
                    .UseResolver(context => // Creating a resolver for a computed property
                    {
                        var parent = context.GetParent<UserDetails>();

                        return $"{parent.LastName}, {parent.FirstName} {parent.MiddleName}";
                    });
            });

        //descriptor.HasProperty("fullName")
        //    .UseResolver(context =>
        //    {
        //        var parent = context.GetParent<User>();

        //        return $"{parent.Details.LastName}, {parent.Details.FirstName}";
        //    });
    }
}