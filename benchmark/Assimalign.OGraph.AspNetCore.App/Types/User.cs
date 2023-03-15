namespace Assimalign.OGraph.AspNetCore;


public class UserType : ComplexType<User>
{
    protected override void Configure(IOGraphComplexTypeDescriptor<User> descriptor)
    {

        descriptor.HasProperty("fullName")
            .UseResolver(context =>
            {
                var parent = context.GetParent<User>();

                return $"{parent.LastName}, {parent.FirstName}";
            });

        descriptor.HasProperty(p => p.FirstName)
            .UseName("first_name")
            .UseMiddleware((context, next) =>
            {


                return next(context);
            })
            .UseMiddleware((context, next) =>
            {


                return next(context);
            })
            .UseResolver(context =>
            {



            });
    }
}

public class User
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public UserDetails Details { get; set; }
}


public class UserDetails
{
    public string Ssn { get; set; }
    public DateTime Birthdate { get; set; }
}