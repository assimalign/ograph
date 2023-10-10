using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Assimalign.OGraph.Execution;

public class OGraphExecutionTests
{
    private readonly IOGraphExecutor executor;

    public OGraphExecutionTests()
    {
        executor = OGraphExecutor.Create("users-domain", (options, builder) =>
        {
            options.RoutePrefix = "api";

            builder.AddNode("Users")
                .UseType<User>(descriptor =>
                {
                    descriptor.HasProperty("fullName")
                        .UseType<StringType>()
                        .UseResolver(context =>
                        {
                            var parent = context.GetParent<User>();

                            return $"{parent.LastName}, {parent.FirstName} {parent.MiddleName}";
                        });

                });

            builder.AddCommand("GetUsers")
                .UseRoute("/users")
                .UseMethod(Method.Get)
                .UseNode("Users")
                .UseResolver(context =>
                {
                    var users = new List<User>()
                    {
                        new User() { FirstName = "Chase", LastName = "Crawford", MiddleName = "Ryan" },
                        new User() { FirstName = "John", LastName = "Doe", MiddleName = null },
                        new User() { FirstName = "Jane", LastName = "Doe", MiddleName = "Henry" }
                    };

                    return users.AsQueryable();
                });
        });
    }

    [Fact]
    public async void Test()
    {

        var query = "project({ fullName })";
        var request = new OGraphTestRequest()
        {
            Method = "GET",
            Path = "/users"
        };

        request.Query.Add("query", query);

        var response = await executor.ExecuteAsync(request, CancellationToken.None);


    }


    public class User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
    }
    public class OGraphTestRequest : IOGraphExecutorRequest
    {
        public OGraphTestRequest()
        {
            this.Query = new OGraphQueryParamCollection();
            this.Headers = new OGraphHeaderCollection();
            this.Body = new MemoryStream();
        }
        public Method Method { get; init; }
        public Path Path { get; init; }
        public Host Host { get; }
        public IOGraphQueryParamCollection Query { get; }
        public IOGraphHeaderCollection Headers { get; }
        public Stream Body { get; }
    }
}
