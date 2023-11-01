using System;
using System.Xml;
using System.Text.Json;

namespace Assimalign.OGraph.Gdm;

/// <summary>
/// Represents collection types such as Arrays, Lists, etc.,
/// </summary>
public interface IOGraphGdmCollectionType : IOGraphGdmType
{
    /// <summary>
    /// Represents the item type that is contained inside the collection.
    /// </summary>
    IOGraphGdmType ItemType { get; }
}
