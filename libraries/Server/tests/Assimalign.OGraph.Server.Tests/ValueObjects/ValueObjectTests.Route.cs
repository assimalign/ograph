using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.ValueObjects;

public partial class ValueObjectTests
{

    [Fact(DisplayName = "Value Object Test (Route): HashCode Match")]
    public void TestHashCodeMatch()
    {
        var route1 = new Route("/UseRs/{userId}");
        var route2 = new Route("/users/{userId}");

        Assert.Equal(2, route1.Segments.Length);
        Assert.True(route1.GetHashCode() == route2.GetHashCode());
        Assert.True(route1.Equals(route2));
    }


    [Fact(DisplayName = "Value Object Test (Route): Route Match")]
    public void RoutePathMatchTestSuccess()
    {
        var route = new Route("/users/{userId}");
        var path = new Path($"/users/{Guid.NewGuid()}");

        Assert.True(route.IsMatch(path));

        var route1 = new Route("/api/users/{userId}");
        var path1 = new Path($"/users/{Guid.NewGuid()}");

        Assert.True(route1.IsMatch(path1, "api"));
    }
}
