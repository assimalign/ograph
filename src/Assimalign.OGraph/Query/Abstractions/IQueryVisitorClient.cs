using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Query;

public interface IQueryVisitorClient
{
    void Execute<T>(RootNode root, IQueryVisitor<T> visitor);
}
