using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Query;

public enum QueryNodeKind
{
    Root,
    Binary,
    Constant,
    Filter,
    Select,
    Sort,
    Member
}