using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Syntax.Visitors;

internal class QueryValidatorVisitor : IQueryNodeVisitor<QueryDocument>
{
    private readonly IOGraphNode node;
    private readonly QueryDocument document;

    public QueryValidatorVisitor(IOGraphNode node, QueryDocument document)
    {
        this.node = node;
        this.document = document;
    }



    public QueryDocument Visit(QueryNode node) => node.Accept(this);

    public QueryDocument Visit(RootQueryNode node)
    {
        //if (node.TryGetProjections(out var projections))
        //{
        //    foreach (var projection in projections)
        //    {
        //        if (projection.IsRoot)
        //        {
        //            projection.Accept(this);
        //        }
        //        else
        //        {
        //            var edge = default(IOGraphEdge);
        //            var paths = projection.Edge.Split('/');

        //            foreach (var path in paths)
        //            {
        //                edge = this.node.Edges.FirstOrDefault(x => x.Name == path);

        //                if (edge is null)
        //                {
        //                    document.AddDiagnostic(new()
        //                    {

        //                    });
        //                }
        //            }

        //            var visitor = new QueryValidatorVisitor(edge.TargetNode, document);

        //            visitor.Visit(projection);
        //        }
        //    }
        //}

        //return document;

        throw new NotImplementedException();
    }

    public QueryDocument Visit(FilterQueryNode node)
    {
        throw new NotImplementedException();
    }

    public QueryDocument Visit(ProjectionQueryNode node)
    {
        throw new NotImplementedException();
    }

    public QueryDocument Visit(PageQueryNode node)
    {
        throw new NotImplementedException();
    }

    public QueryDocument Visit(SortQueryNode node)
    {
        throw new NotImplementedException();
    }

    public QueryDocument Visit(BinaryQueryNode node)
    {
        throw new NotImplementedException();
    }

    public QueryDocument Visit(AttributeQueryNode node)
    {
        throw new NotImplementedException();
    }

    public QueryDocument Visit(PropertyQueryNode node)
    {
        throw new NotImplementedException();
    }

    public QueryDocument Visit(ParameterQueryNode node)
    {
        throw new NotImplementedException();
    }

    public QueryDocument Visit(FunctionQueryNode node)
    {
        throw new NotImplementedException();
    }

    public QueryDocument Visit(ConstantQueryNode node)
    {
        throw new NotImplementedException();
    }
}
