using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Assimalign.OGraph.Tests;

public class OGraphNodeTests
{

    [Fact]
    public void TestPropertyResolver()
    {
        var node = new UserNode(descriptor =>
        {
            descriptor.HasProperty("fullName")
                .UseMiddleware(default)
                .UseResolver(async context =>
                {
                    var user = context.GetParent<User>();

                    return $"{user.LastName}, {user.FirstName}";
                });
        });
    }



    public class UserNode : OGraphNode<User>
    {
        private readonly Action<IOGraphNodeDescriptor<User>> configure;

        public UserNode(Action<IOGraphNodeDescriptor<User>> configure)
        {
            this.configure = configure;
        }

        protected override void Configure(IOGraphNodeDescriptor<User> descriptor)
        {
            configure.Invoke(descriptor);
        }
    }




    public partial class User
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int? Age { get; set; }

        public UserType? UserType { get; set; }

        public IEnumerable<UserAddress> Addresses { get; set; }

    }
    public partial class UserAddress
    {
        public string? StreetOne { get; set; }
        public string? StreetTwo { get; set; }
    }
    public enum UserType
    {

    }
}
