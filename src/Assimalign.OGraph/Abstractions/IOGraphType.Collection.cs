using System;
using System.Xml;
using System.Text.Json;

namespace Assimalign.OGraph;

/// <summary>
/// Represents collection types such as Arrays, Lists, etc.,
/// </summary>
public interface IOGraphCollectionType : IOGraphType
{
    /// <summary>
    /// Represents the item type that is contained inside the collection.
    /// </summary>
    IOGraphType ItemType { get; }
}
