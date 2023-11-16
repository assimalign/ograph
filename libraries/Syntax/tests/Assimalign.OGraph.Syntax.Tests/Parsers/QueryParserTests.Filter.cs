using System;
using Xunit;

namespace Assimalign.OGraph.Syntax;

public partial class QueryParserTests
{
    // filter({firstName eq 'Chase' and ((startsWith(lastName, 'c') or startsWith(lastName, 'd')) or startsWith(middleName, 'e'))})

    [Fact]
    public void ParseFilterSuccessTest()
    {
        var query = """
            filter({firstName eq 'Chase' and ((startsWith(lastName, 'c') or startsWith(lastName, 'd') eq false) and middleName eq 't')})
        """;
        var parser = new QueryParser();
        var document = parser.Parse(query);
    }
}
