using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Syntax;

public partial class QueryParserTests
{
    [Fact]
    public void EdgeParseSuccessTest()
    {
        // Root /employees
        var query = """
            project({
                firstName
                lastName
            })
            .edge(companies as employeeCompanies)
                .project({
                    companyName
                })
                .edge(companies/addresses)
                    .project({
                        streetOne
                        streetTwo
                    })
            """;


        var parser = new QueryParser();
        var document = parser.Parse(query);
    }
}
