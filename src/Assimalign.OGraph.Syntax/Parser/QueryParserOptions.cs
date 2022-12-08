using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Syntax;

public sealed class QueryParserOptions
{
    private readonly IList<IQueryNodeVisitor> visitors = new List<IQueryNodeVisitor>();

    public QueryParserOptions()
    {

    }

    /// <summary>
    /// 
    /// </summary>
    public IEnumerable<IQueryNodeVisitor> Visitors => this.visitors;


    /// <summary>
    /// 
    /// </summary>
    /// <param name="visitor"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public void AddVisitor(IQueryNodeVisitor visitor) 
    {
        if (visitor == null)
        {
            throw new ArgumentNullException(nameof(visitor));
        }
        
        this.visitors.Add(visitor);
    }
}
