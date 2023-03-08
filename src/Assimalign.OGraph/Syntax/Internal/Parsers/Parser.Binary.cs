using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Syntax.Internal;

internal class BinaryParser : Parser
{
    internal override QueryNode Parse(ref TokenLexer lexer, ParserContext context, QueryNode queryNode)
    {
        if (queryNode is not BinaryQueryNode binaryNode)
        {
            // TODO: Add diagnostics
            return queryNode;
        }
        if (binaryNode.LeftOperand is null)
        {
            // TODO: Add diagnostic (A left operand must be supplied)
            return queryNode;
        }



        return queryNode;
    }
}
