
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Syntax;

public sealed partial class QueryParser
{
    partial class ParserContext
    {
        private readonly Queue<Diagnostic> diagnostics = new();

        internal ParserContext() { }

        internal int Depth { get; set; }
        internal string Path { get; set; } = "/";
        internal QueryNode? Parent { get; set; }
        internal bool ThrowExceptionOnDiagnosticError { get; init; }
        internal Encoding Encoding { get; init; } = Encoding.UTF8;
        internal IEnumerable<Diagnostic> Diagnostics => this.diagnostics;
        internal void AddDiagnostic(Diagnostic diagnostic)
        {
            diagnostics.Enqueue(diagnostic);
        }

    }
}
