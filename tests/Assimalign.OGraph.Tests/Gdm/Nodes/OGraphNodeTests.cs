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
