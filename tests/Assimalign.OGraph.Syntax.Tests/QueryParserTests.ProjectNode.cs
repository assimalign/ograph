using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Syntax.Tests;

public partial class QueryParserTests
{

    [Fact]
    public void SelectTest()
    {
        var query = "query().project({ firstName lastName })";
        var parser = new QueryParser();
        var node = parser.Parse(query);
    }
}
