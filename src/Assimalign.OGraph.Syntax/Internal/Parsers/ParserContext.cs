using System;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace Assimalign.OGraph.Syntax.Internal;

internal class ParserContext
{
    private readonly List<QueryDiagnostic> diagnostics = new();
    private static readonly ConcurrentDictionary<Type, Parser> parsers = new();

    internal ParserContext() { }

    internal IList<QueryDiagnostic> Diasgnostics => this.diagnostics;

    internal TParser GetParser<TParser>() where TParser : Parser, new() => (TParser)parsers.GetOrAdd(typeof(TParser), type => new TParser());
    
}
