using Assimalign.OGraph;
using Assimalign.OGraph.AspNetCore;
using System.Threading;


var builder = WebApplication.CreateBuilder(args);


builder.Services
    .AddSingleton<IRepository<User>, UserRepository>();

builder.Services
    .AddOGraphOptions(options =>
    {
        options.RoutePrefix = "api";
    })
    .AddOGraph("users", builder =>
    {
        // Composite Implementation
        builder.AddVertex<User>(vertex =>
        {
            // Define Type information
            vertex.HasLabel("user");
            vertex.HasProperty(p => p.Details)

                    .UseType(details => // Extending the details type
                    {
                        details.HasProperty("fullName")
                            .UseMiddleware(async (context, next) =>
                            {
                                if (!context!.GetClaimsPrincipal()!.Identity!.IsAuthenticated)
                                {
                                    return OGraphResult.Unauthorized("The user is not authorized to access this resource.");
                                }
                                return await next(context);
                            })
                            .UseResolver(context => // Creating a resolver for a computed property
                            {
                                var parent = context.GetParent<UserDetails>();

                                return $"{parent.LastName}, {parent.FirstName} {parent.MiddleName}";
                            });
                    });

            // Define Edges
            vertex.HasEdge("addresses") //There should be a corresponding operation bound to the addresses vertex
                .WithMany<UserAddressVertex>();

            vertex.HasEdge("primaryAddress")
                .WithOne<UserAddressVertex>();

            vertex.HasEdge("profile")
                .WithOne<UserProfileVertex>();
        });
        builder.AddVertex<UserAddress>(vertex =>
        {
            vertex.HasLabel("address");
        });
        builder.AddVertex<UserProfile>(vertex =>
        {
            vertex.HasLabel("profile");

        });
    });

var app = builder.Build();

app.MapGet("", async context =>
{

});

app.UseOGraph();
app.Run();
