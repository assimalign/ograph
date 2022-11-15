using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Syntax;

public struct TokenLexerOptions
{
    public bool SkipTabs { get; set; }
    public bool SkipWhiteSpace { get; set; }
    public bool SkipLineFeed { get; set; }
    public bool SkipCarriageReturn { get; set; }
}
