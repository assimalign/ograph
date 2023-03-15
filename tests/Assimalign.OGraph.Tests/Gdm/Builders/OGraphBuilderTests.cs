using Assimalign.OGraph.Syntax;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Assimalign.OGraph.Tests;


public class OGraphBuilderTests
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
            4. Execute Root Property Resolver
            5. Execute Edge Resolver
         
         */

        var graph = OGraphBuilder.Create("UsersDomain", builder =>
        {
            // Define Root Structures

            // Fluent Implementation
            //    builder.AddNode<User>("users", descriptor =>
            //    {
            //        descriptor.HasProperty("FullName")
            //            .UseResolver(async context =>
            //            {
            //                var user = context.GetParent<User>();


            //                return $"{user.LastName}, {user.FirstName} {user.MiddleName}";
            //            });

            //        descriptor.HasProperty(p => p.Password)
            //            .UseName("UserPassword")
            //            .UseMetadata("description", "")
            //            .UseResolver(context =>
            //            {


            //                return string.Empty;
            //            });

            //        descriptor.HasProperty(p => p.Profile)
            //            .UseName("UserProfile")
            //            .UseMiddleware((context, next) =>
            //            {


            //                return next(context);
            //            })
            //            .UseMiddleware((context, next) =>
            //            {


            //                return next(context);
            //            })
            //            .UseResolver(context =>
            //            {   
            //                return context.GetParent<User>().Profile;
            //            });


            //        //// Define Edges:
            //        //// One-to-One Relationship: /users/{userId}/profiles/{profileId} 
            //        //descriptor.HasEdge(p => p.Profile)                    
            //        //    .UseMiddleware(default)
            //        //    .UseResolver(async context =>
            //        //    {
            //        //        return default;
            //        //    });

            //        //// One-to-Many Relationship: /users/{userId}/addresses
            //        //descriptor.HasEdge(p => p.Addresses)
            //        //    .UseResolver(async context =>
            //        //    {
            //        //        return default;
            //        //    });
            //    });
            //    //builder.AddNode<UserAddress>("addresses", descriptor =>
            //    //{

            //    //});
            //    //builder.AddNode("settings", descriptor =>
            //    //{
            //    //    descriptor.HasProperty("addressType");

            //    //});
            //    //builder.AddNode<UserAddressNode>();


            //    //builder.AddOperation("CreateUser")
            //    //    .UseMethod(Method.Post)
            //    //    .UseRoute("/api/users");

            //    //builder.AddOperation("GetUsers")
            //    //    .UseMethod(Method.Get)
            //    //    .UseRoute("/api/users")
            //    //    .UseMiddleware(async (context, next) =>
            //    //    {


            //    //        return await next(context);
            //    //    })
            //    //    .UseResolver(async context =>
            //    //    {

            //    //    });

        });
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
