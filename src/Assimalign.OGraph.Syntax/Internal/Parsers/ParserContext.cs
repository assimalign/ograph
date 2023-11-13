using System;
using System.Text;
using System.Collections.Generic;

namespace Assimalign.OGraph.Syntax.Internal;

internal class ParserContext
{
    private readonly Queue<Diagnostic> diagnostics = new();
    private static readonly Dictionary<Type, Parser> parsers = new();

    internal ParserContext() { }

    internal VertexNode Root { get; init; }
    internal bool ThrowExceptionOnDiagnosticError { get; init; }
    internal Encoding Encoding { get; init; } = Encoding.UTF8;
    internal IEnumerable<Diagnostic> Diagnostics => this.diagnostics;
    internal TParser GetParser<TParser>() where TParser : Parser, new()
    {
        var type = typeof(TParser);
        
        if (parsers.ContainsKey(type))
        {
            return (TParser)parsers[type];
        }

        var parser = new TParser();

        parsers.Add(type, parser);

        return parser;
    }
    internal void AddDiagnostic(Diagnostic diagnostic)
    {
        diagnostics.Enqueue(diagnostic);
    }
}
