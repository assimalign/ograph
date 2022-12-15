using System;
using System.Collections.Generic;
using System.Linq;

namespace Assimalign.OGraph.Syntax;

/// <summary>
/// 
/// </summary>
public sealed class QueryDocument
{
	internal QueryDocument(QueryNode node, IEnumerable<QueryDiagnostic> diagnostics)
	{
		this.Node = node;
		this.Diagnostics = diagnostics;
	}

	/// <summary>
	/// 
	/// </summary>
	public bool IsValid => Errors.Any();
	/// <summary>
	/// Represents the root node of the OGraph Query.
	/// </summary>
    public QueryNode Node { get; }
	/// <summary>
	/// 
	/// </summary>
	public IEnumerable<QueryDiagnostic> Diagnostics { get; }
	/// <summary>
	/// 
	/// </summary>
	public IEnumerable<QueryDiagnostic> Errors => Diagnostics.Where(x => x.Severity == QueryDiagnosticSeverity.Error);
}
