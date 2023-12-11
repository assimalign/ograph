using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Assimalign.OGraph.Syntax;

public partial class QueryParserTests
{

    [Fact(DisplayName = "Parser Test (Projections): Complete Successful Parse")]
    public void ProjectionSuccessTest()
    {
        var query = """
            vertex('test')
            .project({ 
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
                }
            })
        """;
        var parser = new QueryParser();
        var document = parser.Parse(query);

        // Assert No Errors
        Assert.Empty(document.Errors);


        var root = Assert.IsType<VertexNode>(document.Root);
        var node = Assert.Single(root.Nodes);
        var projection = Assert.IsType<ProjectionNode>(node);

        Assert.Equal(4, projection.Properties.Count());

        // Asset Alias is set for Addresses
        Assert.Equal("locations", projection.Properties.FirstOrDefault(x => x.Name == "addresses")?.Alias);
        Assert.Equal(2, projection.Properties.FirstOrDefault(x => x.Name == "addresses")?.Children?.Count());

        // Assert third nested field is set
        Assert.Single(projection.Properties.FirstOrDefault(x => x.Name == "field1")?
            .Children?.FirstOrDefault(x => x.Name == "nf1")?
            .Children);
    }

    [Fact(DisplayName = "Parser Test (Projections): G0001 - Missing Closing Bracket")]
    public void ProjectionMissingOpeningParenthesisTest()
    {
        var query = """
            project { # Parenthesis is missing on this line
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
                }
            })
        """;
        var parser = new QueryParser();
        var document = parser.Parse(query);

        // Assert No Errors
        Assert.NotEmpty(document.Errors);
        Assert.Contains(document.Errors, error => error.Code == DiagnosticCode.G0001.ToString());
    }

    [Fact(DisplayName = "Parser Test (Projections): G0002 - Missing Closing Bracket")]
    public void ProjectionMissingClosingParenthesisTest()
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
                }
            } # Parenthesis is missing on this line
        """;
        var parser = new QueryParser();
        var document = parser.Parse(query);

        // Assert No Errors
        var error = Assert.Single(document.Errors);

        Assert.Contains(error.Code, DiagnosticCode.G0002.ToString());
    }

    [Fact(DisplayName = "Parser Test (Projections): G0004 - Missing Closing Bracket")]
    public void ProjectionMissingClosingBracketTest()
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
                # Bracket is missing on this line
            })
        """;
        var parser = new QueryParser();
        var document = parser.Parse(query);

        // Assert No Errors
        Assert.Equal(3, document.Errors.Count());

        Assert.Contains(document.Errors, diagnostic => diagnostic.Code == DiagnosticCode.G0002.ToString());
        Assert.Contains(document.Errors, diagnostic => diagnostic.Code == DiagnosticCode.G0004.ToString());
    }

    [Fact(DisplayName = "Parser Test (Projections): G0003 - Missing Opening Bracket")]
    public void ProjectionMissingOpeningBracketTest()
    {
        var query = """
            project(employees, # Bracket is missing on this line
                firstName
                lastName 
                addresses as locations { 
                    streetOne as mainStreet 
                    streetTwo 
                } 
                field1 as testAlias { 
                    nf1 as nf1a  {
                        nf2 
                    }
                }
            })
        """;
        var parser = new QueryParser();
        var document = parser.Parse(query);

        // Assert No Errors
        Assert.NotEmpty(document.Errors);

        Assert.Contains(document.Errors, diagnostic => diagnostic.Code == DiagnosticCode.G0003.ToString());
    }

    [Fact(DisplayName = "Parser Test (Projections): Skip Comma Separator in projections")]
    public void TestSkipCommaSeparatorInProject()
    {
        var query = """
            project({ 
                firstName, 
                lastName
                addresses as locations, { 
                    streetOne as mainStreet 
                    streetTwo
                } 
                field1 as testAlias { 
                    nf1 as nf1a { 
                        nf2 
                    }
                }
            })
        """;
        var parser = new QueryParser();
        var document = parser.Parse(query);

        // Assert No Errors
        Assert.Empty(document.Errors);

        var root = Assert.IsType<VertexNode>(document.Root);
        var node = Assert.Single(root.Nodes);
        var projection = Assert.IsType<ProjectionNode>(node);

        Assert.Equal(4, projection.Properties.Count());

    }
}
