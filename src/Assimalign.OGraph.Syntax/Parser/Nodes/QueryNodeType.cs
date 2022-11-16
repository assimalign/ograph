using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Syntax;
public enum QueryNodeType
{
    Select,
    Filter,
    Sort,
    Page,

    Member,
    Function,
    Parameter,
    Constant,
    Binary,
}

