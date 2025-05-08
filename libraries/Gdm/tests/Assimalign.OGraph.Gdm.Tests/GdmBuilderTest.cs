using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Assimalign.OGraph.Gdm.Tests;

public class GdmBuilderTest
{
    [Fact]
    public void Test()
    {
        var model = GdmBuilderUtility.Create(GdmBuilderUtilityStrategy.Fluent);
    }
}
