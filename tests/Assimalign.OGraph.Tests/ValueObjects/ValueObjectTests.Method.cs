using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Assimalign.OGraph.ValueObjects;

public partial class ValueObjectTests
{
    [Fact]
    public void TestBadMethodName()
    {
        Assert.ThrowsAny<Exception>(() =>
        {
            Method method = "GET1";
        });
    }
}
