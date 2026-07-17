using System;
using System.Xml;
using System.Text.Json;
using System.Diagnostics.CodeAnalysis;

namespace Assimalign.OGraph.Gdm;

/// <summary>
/// Types represent primitive, complex, collections, or enum structure that can be 
/// used to define a property, inputs, and outputs of operations within the graph.
/// </summary>
/// <remarks>
/// An <see cref="IOGraphGdmType"/> represents a
/// </remarks>
public interface IOGraphGdmType : IOGraphGdmNamedElement
{
    /// <summary>
    /// The identifier of the type.
    /// </summary>
    GdmTypeKind Kind { get; }

    /// <summary>
    /// The Graph in which the type belongs to.
    /// </summary>
    IOGraphGdmGraph Graph { get; }
}