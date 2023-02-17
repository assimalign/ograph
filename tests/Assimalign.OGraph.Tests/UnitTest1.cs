using Assimalign.OGraph.Syntax;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Assimalign.OGraph.Tests
{

    

    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            /*
             * 
                There three types of Resolvers: 
            
                1. Operation Resolver
                2. Property Resolver
                3. Edge Resolver
                3.1 Edge Property Resolver

                Execution Pipeline:

                1. Parse HTTP Request
                2. Validate HTTP Request
                3. Execute OnResolve Middleware (Pre-Resolver)  
                3. Execute Operation Resolver
                4. Execute Node Property Resolver
                5. Execute Edge Resolver
             
             */

            var query = default(QueryDocument);
            var graph = OGraphBuilder.Create("UsersDomain", builder =>
            {
                // Define Node Structures

                // Fluent Implementation
                builder.AddNode<User>("users", descriptor =>
                {
                    descriptor.HasProperty("FullName")
                        .UseType<StringType>()
                        .UseResolver(async context =>
                        {
                            var user = context.GetParent<User>();


                            return $"{user.LastName}, {user.FirstName} {user.MiddleName}";
                        });

                    descriptor.HasProperty(p => p.Password)
                        .UseName("UserPassword")
                        .UseType<StringType>()
                        .UseMetadata("description", "")
                        .UseResolver(async context =>
                        {


                            return string.Empty;
                        });

                    descriptor.HasProperty(p => p.Profile)
                        .UseName("UserProfile");


                    //// Define Edges:
                    //// One-to-One Relationship: /users/{userId}/profiles/{profileId} 
                    descriptor.HasEdge(p => p.Profile)
                        .UseMiddleware(default)
                        .UseResolver(async context =>
                        {
                            return default;
                        });

                    //// One-to-Many Relationship: /users/{userId}/addresses
                    //descriptor.HasEdge(p => p.Addresses)
                    //    .UseResolver(async context =>
                    //    {
                    //        return default;
                    //    });
                });
                builder.AddNode<UserAddress>("addresses", descriptor =>
                {

                });
                builder.AddNode("settings", descriptor =>
                {
                    descriptor.HasProperty("addressType")
                        .HasResolver(async context =>
                        {

                            

                        });

                });
                

                builder.AddOperation("CreateUser", descriptor =>
                {
                    descriptor.UseMiddleware()
                })
                //// Inheritance Implementation
                //builder.AddNode<UserAddressNode>();
                

                //builder.AddNode<UserProfile>("Profiles", descriptor =>
                //{

                //});

                //builder.AddNode("Interests", descriptor =>
                //{
                //    descriptor
                //        .HasProperty("interestId")
                //        .UseResolver(async context =>
                //        {

                //        });

                //});



                //// Link/Bind Operations (Endpoints) to Nodes Structures
                //builder.AddOperation("UserCreate", descriptor =>
                //{
                //    descriptor.UseMethod("POST")
                //        .UseRoute("/users")
                //        .UseNodes("Users")
                //        .UseAuthorization()
                //        .UseResolver(async context =>
                //        {

                //        });
                //});
            });

            //var list = new List<User>();

            //foreach (var user in list)
            //{
            //    if (query.Node is not RootQueryNode root)
            //    {
            //        throw new Exception();
            //    }
            //    if (root.TryGetSelectNode(out var projections))
            //    {
            //        projections.
            //    }
                
            //    var node = graph.Nodes[0];

            //    foreach (var property in node.Properties)
            //    {
            //        property.PropertyType.TypeResolver.Invoke()
            //    }

            //    node.Edges[0].EdgeResolver.
            //}

            
        }

        public class UserAddressNode : OGraphNode<UserAddress>
        {
            protected override void Configure(IOGraphNodeDescriptor<UserAddress> descriptor)
            {
                descriptor.HasKey(p => p.AddressId);
            }
        }
    }

    #region Test Models
    public class User
    {
        public Guid? UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        // Edges
        public UserProfile Profile { get; set; }
        public IEnumerable<UserAddress> Addresses { get; set; }
    }
    public class UserAddress
    {
        public Guid? AddressId { get; set; }
        public string StreetOne { get; set; }
        public string StreetTwo { get; set; }
        public string StreetThree { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public UserAddressType AddressType { get; set; }

    }
    public enum UserAddressType
    {
        Primary,
        Secondary,
        Home,
        Mailing
    }
    public class UserProfile
    {

    }
    
    #endregion
}