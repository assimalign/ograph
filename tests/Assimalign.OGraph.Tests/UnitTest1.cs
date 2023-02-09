namespace Assimalign.OGraph.Tests
{

    public class User
    {
        
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var builder = default(IOGraphBuilder);

            builder.AddQuery("GetUsers", descriptor =>
            {
                descriptor.UseMethod("GET")
                    .UseRoute("/users")
                    .UseFiltering()
                    .UsePaging()
                    .UseSorting()
                    .UseAuthorization()
                    .UseQueryableType<User>(type =>
                    {
                        type.UseName("UsersCollection");


                    })
                    .UseResolver(async context =>
                    {

                    });
            });
            builder.AddQuery("GetUserById", descriptor =>
            {
                descriptor.UseMethod("GET")
                    .UseRoute("/users/{userId}")
                    .UseFiltering()
                    .UsePaging()
                    .UseSorting()
                    .UseAuthorization()
                    .UseType<User>(type =>
                    {

                    })
                    .UseResolver(async context =>
                    {

                    });
            });
            builder.AddQuery("GetUsersAppRoleAssignments", descriptor =>
            {
                descriptor.UseMethod("GET")
                    .UseRoute("/users/{userId}/appRoleAssignments")
        
    });
            builder.AddCommand("CreateUsers", descriptor =>
            {
                descriptor.UseMethod("POST")
                    .UseRoute("/users");
            });
        }
    }
}