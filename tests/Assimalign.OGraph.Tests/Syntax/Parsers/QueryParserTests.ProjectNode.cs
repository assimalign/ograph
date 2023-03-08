using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Syntax;

public partial class QueryParserTests
{

    [Fact]
    public void ProjectionSuccessTest()
    {
        var query = """
            project({ 
                firstName 
                lastName 
                addresses as locations { 
                    streetOne as mainStreet 
                    streetTwo 
                } 
                field1 as testAlias { 
                nf1 as nf1a { 
                    nf2 
                }
            })
        """;
        var parser = new QueryParser();
        var document = parser.Parse(query);
    }

    [Fact]
    public void TestSkipCommaSeparatorInProject()
    {
        var query = "query().project({ firstName, lastName addresses as locations, { streetOne as mainStreet streetTwo } field1 as testAlias { nf1 as nf1a { nf2 }})";
        var parser = new QueryParser();
        var node = parser.Parse(query);
    }


    #region Edge Projection Parsing Tests

    [Fact]
    public void TestEdgeProjectionParseSuccessTest()
    {
        var query = """
            project(employees/addresses, {
                streetOne
                streetTwo
                streetThree
                addressTypes {
                    typeId
                    type as addressType
                }
            })
        """;

        var parser = new QueryParser();
        var document = parser.Parse(query);

        // Assert No Errors
        Assert.Empty(document.Errors);

        var root = Assert.IsType<RootQueryNode>(document.Node);
        var projection = Assert.IsType<ProjectionQueryNode>(root.Nodes.First());

        Assert.Equal("employees/addresses", projection.Edge.Path);
        Assert.False(projection.IsRoot);
    }

    [Fact]
    public void TestEdgeProjectionExpectedCommaFailureTest()
    {
        var query = """
            project(employees/addresses {
                streetOne
                streetTwo
                streetThree
                addressTypes {
                    typeId
                    type
                }
            })
        """;


        var parser = new QueryParser();
        var document = parser.Parse(query);

        // Assert Errors: Missing single comma
        Assert.Single(document.Errors);

        var root = Assert.IsType<RootQueryNode>(document.Node);
        var projection = Assert.IsType<ProjectionQueryNode>(root.Nodes.First());

        Assert.Equal("employees/addresses", projection.Edge.Path);
        Assert.False(projection.IsRoot);
    }

    #endregion
}
