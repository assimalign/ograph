using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Syntax.Tests;

public partial class QueryParserTests
{
    [Fact]
    public void TestUnexpectedTokenErrorFromRoot()
    {
        var query = "some identifier not expected";
        var parser = new QueryParser();
        var document = parser.Parse(query);

        Assert.Equal(4, document.Errors.Count());
    }
}
