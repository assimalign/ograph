using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Assimalign.OGraph;

public partial class ValueObjectTests
{
    [Fact(DisplayName = "Value Object Test: InValid Status Code")]
    public void InValidStatusCodeTest()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            StatusCode statusCode = 5000;
        });
    }


    [Fact(DisplayName = "Value Object Test: IComparable Tests")]
    public void IComparableTest()
    {
        StatusCode statusCode1 = 201;
        StatusCode statusCode2 = 200;

        Assert.True(statusCode1 > statusCode2);
        Assert.True(statusCode1 >= statusCode2);
        Assert.True(statusCode2 < statusCode1);
        Assert.True(statusCode2 <= statusCode1);

        StatusCode statusCode3 = 200;
        StatusCode statusCode4 = 200;

        Assert.True(statusCode3 >= statusCode4);
        Assert.True(statusCode3 <= statusCode4);
        Assert.True(statusCode4 >= statusCode3);
        Assert.True(statusCode4 <= statusCode3);
    }
}
