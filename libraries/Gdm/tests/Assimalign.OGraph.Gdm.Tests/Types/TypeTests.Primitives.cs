using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Assimalign.OGraph.Gdm.Tests;



public partial class TypeTests
{

    [Theory]
    [InlineData(GdmBuilderUtilityStrategy.Fluent)]
    [InlineData(GdmBuilderUtilityStrategy.Composable)]
    [InlineData(GdmBuilderUtilityStrategy.Mixed)]
    public void MyTest(GdmBuilderUtilityStrategy strategy)
    {
        var model = GdmBuilderUtility.Create(strategy);
    }
}
