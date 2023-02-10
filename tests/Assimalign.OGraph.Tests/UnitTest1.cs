using Assimalign.OGraph.Modeling;
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
    }

    public class UnitTest1
    {


        public void Test2()
        {
            var builder = default(IOGraphModelBuilder);

            builder.AddNode<User>(descriptor =>
            {
                descriptor.AddOperation("GetUsers", operation =>
                {
                    operation.UseRoute("/users")
                        .UseMethod("GET")
                        .UseRequestType<User>(type =>
                        {
                            type.UseResolver()
                        });
                });

                

                
                descriptor.AddType(descriptor =>
                {

                });
                descriptor.AddType(descriptor =>
                {
                    
                });
            });
        }
        [Fact]
        public void Test1()
        {
            var builder = default(IOGraphBuilder) ?? throw new Exception();

            builder.AddNode<User>(descriptor =>
            {
                descriptor
                    .AddName("UserName")
                    .AddOperation("GetUsers", operation =>
                    {
                        operation
                            .UseFiltering()
                            .UseSorting()
                            .UsePaging()
                            .UseMethod("GET")
                            .UseRoute("/users")
                            .UseEdge("addresses", edge =>
                            {
                                edge.
                            })
                            .Use
                            .UseResponseType(descriptor =>
                            {
                                descriptor
                                    .UseResolver(context =>
                                    {

                                    });
                            });
                    })
                    .AddOperation("GetUserById", operation =>
                    {

                    })
                    .AddOperation("CreateUser", operation =>
                    {

                    })
                    .AddOperation("UpdateUser", operation =>
                    {

                    })
                    .AddType(descriptor=>
                    {
                        descriptor.
                    });
            });
        }
    }
}