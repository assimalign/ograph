using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace Assimalign.OGraph.Syntax;

/// <summary>
/// 
/// </summary>
public sealed class QueryDocument
{
	private readonly ConcurrentBag<Diagnostic> diagnostics;
	internal QueryDocument(
		string text,
		QueryNode root, 
		IEnumerable<Diagnostic> diagnostics)
	{
		this.Text = text;
		this.Root = root;
		this.diagnostics = new ConcurrentBag<Diagnostic>(diagnostics);
	}
	/// <summary>
	/// The raw text of the OGraph Query.
	/// </summary>
	public string Text { get; }
	/// <summary>
	/// Specifies whether the query has any errors.
	/// </summary>
	public bool IsValid => !Errors.Any();
	/// <summary>
	/// Represents the root of the OGraph query.
	/// </summary>
	public QueryNode Root { get; }
	/// <summary>
	/// A collection of diagnostic information for the parsed OGraph query.
	/// </summary>
	public IEnumerable<Diagnostic> Diagnostics => this.diagnostics.OrderBy(x => x.Severity).ThenBy(x => x.End);
	/// <summary>
	/// Represents a collection of diagnostic errors.
	/// </summary>
	public IEnumerable<Diagnostic> Errors => Diagnostics.Where(x => x.Severity == DiagnosticSeverity.Error);
	/// <summary>
	/// 
	/// </summary>
	/// <param name="diagnostic"></param>
	/// <exception cref="ArgumentNullException"></exception>
	public void AddDiagnostic(Diagnostic diagnostic)
	{
		if (diagnostics is null)
		{
			throw new ArgumentNullException(nameof(diagnostic));
		}
		diagnostics.Add(diagnostic);
	}

}
