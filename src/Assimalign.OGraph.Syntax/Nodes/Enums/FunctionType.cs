using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Syntax;

public enum FunctionType
{
    None,
    // string functions
    StartsWith,
    EndsWith,
    Contains,
    Concat,
    SubString,
    PadRight,
    PadLeft,
    Trim,
    TrimRight,
    TrimLeft,

    // array functions (with scalar output only)
    Any,
    All,

    // other functions
    In,
}