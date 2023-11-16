using System;
using Xunit;

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
