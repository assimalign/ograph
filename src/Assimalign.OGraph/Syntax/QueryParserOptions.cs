using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Syntax;

public sealed class QueryParserOptions
{
    private IList<Func<QueryNode, object>> visitors;


    public QueryParserOptions()
    {
        this.visitors = new List<Func<QueryNode, object>>();
    }

    /// <summary>
    /// Throws an exception when there is a Diagnostic Error. The default is 'false'.
    /// </summary>
    public bool ThrowExceptionOnDiagnosticError { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public Encoding Encoding { get; set; } = Encoding.UTF8;


    internal IList<Func<QueryNode, object>> Visitors => this.visitors;


    public void AddVisitor<T>(IQueryNodeVisitor<T> visitor)
    {
        visitors.Add(node =>
        {
            return visitor.Visit(node);
        });
    }
}
