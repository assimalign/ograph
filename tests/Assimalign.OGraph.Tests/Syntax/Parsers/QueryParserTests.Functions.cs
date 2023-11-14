using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Assimalign.OGraph.Syntax;

public partial class QueryParserTests
{


    public void AndBinaryTest()
    {
        var query = """
            filter({
                and(
                    eq(firstName, 'John'),
                    eq(lastName, 'Doe')
                )
            })

        """;
        var parser = new QueryParser();
        var document = parser.Parse(query);

    }



}
