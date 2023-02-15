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
            
            1. Node Property Resolver
            2. Edge Resolver
            3. Operation Resolver

            Execution Pipeline:

            1. Parse HTTP Request
            2. Validate HTTP Request
            3. Execute OnResolve Middleware (Pre-Resolver)  
            3. Execute Operation Resolver
            4. Execute Node Property Resolver
            5. Execute Edge Resolver
             
             */
            var graph = OGraphBuilder.Create("UsersDomain", builder =>
            {
                // Define Node Structures

                // Fluent Implementation
                builder.AddNode<User>("Users", descriptor =>
                {
                    descriptor.HasKey(p => p.UserId);

                    descriptor.HasProperty("FullName")
                        .HasType<StringType>()
                        .HasResolver(async context =>
                        {
                            var user = context.GetParent<User>();


                            return $"{user.LastName}, {user.FirstName} {user.MiddleName}";
                        });
                    
                    descriptor.HasProperty(p => p.Password)
                        .HasName("UserPassword")
                        .HasType<StringType>()
                        .HasResolver(async context =>
                        {


                            return string.Empty;
                        });

                    // One-to-One Relationship: /users/{userId}/profiles/{profileId} 
                    descriptor.HasEdge(p => p.Profile)
                        .HasResolver(async context =>
                        {
                            return default;
                        });

                    // One-to-Many Relationship: /users/{userId}/addresses
                    descriptor.HasEdge(p => p.Addresses)
                        .HasResolver(async context =>
                        {
                            return default;
                        });
                });
                
                // Inheritance Implementation
                builder.AddNode<UserAddressNode>();
                

                builder.AddNode<UserProfile>("Profiles", descriptor =>
                {

                });



                // Link Operations (Endpoints) to Nodes Structures
                builder.AddOperation("UserCreate", descriptor =>
                {
                    descriptor.UseMethod("POST")
                        .UseRoute("/users")
                        .UseNodes("Users")
                        .UseAuthorization()
                        .UseResolver(async context =>
                        {

                        });
                });
            });
        }

        public class UserAddressNode : OGraphNode<UserAddress>
        {
            public override void Configure(IOGraphNodeDescriptor<UserAddress> descriptor)
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