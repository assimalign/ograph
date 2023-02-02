using System;
using System.Linq;
using System.Collections.Generic;

namespace Assimalign.OGraph.Syntax;

/// <summary>
/// 
/// </summary>
public sealed class QueryDocument
{
	internal QueryDocument(QueryNode node, IEnumerable<Diagnostic> diagnostics)
	{
		this.Node = node;
		this.Diagnostics = diagnostics;
	}

	/// <summary>
	/// 
	/// </summary>
	public bool IsValid => !Errors.Any();
	/// <summary>
	/// Represents the root node of the OGraph Query.
	/// </summary>
    public QueryNode Node { get; }
	/// <summary>
	/// 
	/// </summary>
	public IEnumerable<Diagnostic> Diagnostics { get; }
	/// <summary>
	/// 
	/// </summary>
	public IEnumerable<Diagnostic> Errors => Diagnostics.Where(x => x.Severity == DiagnosticSeverity.Error);
}
