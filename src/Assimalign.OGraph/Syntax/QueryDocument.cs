using System;
using System.Linq;
using System.Collections.Generic;

namespace Assimalign.OGraph.Syntax;

/// <summary>
/// 
/// </summary>
public sealed class QueryDocument
{
	private readonly IList<Diagnostic> diagnostics;
	internal QueryDocument(QueryNode node, IEnumerable<Diagnostic> diagnostics)
	{
		this.Node = node;
		this.diagnostics = diagnostics.ToList();
	}

	/// <summary>
	/// Specifies whether the query has any errors.
	/// </summary>
	public bool IsValid => !Errors.Any();
	/// <summary>
	/// Represents the root node of the OGraph Query.
	/// </summary>
	public QueryNode Node { get; }
	/// <summary>
	/// 
	/// </summary>
	public IEnumerable<Diagnostic> Diagnostics => this.diagnostics;
	/// <summary>
	/// Represents a collection of diagnostic errors.
	/// </summary>
	public IEnumerable<Diagnostic> Errors => Diagnostics.Where(x => x.Severity == DiagnosticSeverity.Error);



	public void AddDiagnostic(Diagnostic diagnostic)
	{
		if (diagnostics is null)
		{
			throw new ArgumentNullException(nameof(diagnostic));
		}
		diagnostics.Add(diagnostic);
	}
}
