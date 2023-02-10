using System.Text.Json;
using System.Text.Json.Nodes;

namespace Assimalign.OGraph.Tests
{

    public class User
    {
        public Guid? UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public IEnumerable<UserAddress> Addresses { get; set; }
    }

    public class UserAddress
    {
       public  Guid? AddressId { get; set; }
    }

    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var graph = OGraphBuilder.Create("UsersDomain", builder =>
            {
                // Node built off of Type
                builder.AddNode<User>("Users", descriptor =>
                {
                    // Define Operations
                    descriptor
                        .AddOperation("GetUsers", operation =>
                        {
                            operation
                                .UseFiltering()
                                .UseSorting()
                                .UsePaging()
                                .UseMethod("GET")
                                .UseRoute("/users")
                                .UseQueryableResponseType("UserCollection", descriptor =>
                                {
                                    descriptor.Ignore(p => p.Addresses);
                                    descriptor.UseResolver(p => p.Addresses, (user, context) =>
                                    {
                                        return context.GetQueryable<UserAddress>(p => p.UserId == user.UserId);
                                    });
                                })
                                .UseResolver(async context =>
                                {


                                    return default(IOGraphOperationResult);
                                });
                        })
                        .AddOperation("GetUserById", operation =>
                        {
                            operation
                                .UseMethod("GET")
                                .UseRoute("/users/{userId}");

                        })
                        .AddOperation("CreateUser", operation =>
                        {
                            operation
                                .UseMethod("PUT")
                                .UseRoute("/users/{userId}")
                                .UseRequestType<User>("", descriptor =>
                                {
                                    descriptor.UseResolver(context =>
                                    {

                                    });
                                })


                        })
                        .AddOperation("UpdateUser", operation =>
                        {
                            operation
                                .UseMethod("PUT")
                                .UseRoute("/users/{userId}")
                                .UseRequestType("UserUpdateInput") // Ths will correlate to the type defined 

                                .UseResolver(async context =>
                                {

                                    var userId = context.GetRouteValue<Guid?>("userId");


                                    return default(IOGraphOperationResult);
                                });
                        });

                    // Define Edges
                    descriptor
                        .AddEdge<UserAddress>("addresses", edge => // In order to add an edge the EdgeName/Type needs to map to another node.
                        {
                            edge


                        });

                    // Define Structures:
                    // Often times we want to limit the properties 
                    descriptor
                        .AddType("UserUpdateInput", descriptor =>
                        {

                        });
                });
                builder.AddNode<UserAddress>("UserAddress", descriptor =>
                {

                });
                builder.AddNode("groups", descriptor =>
                {

                });
            });
        }
    }
}