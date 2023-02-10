using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

/// <summary>
/// 
/// </summary>
public interface IOGraphCollectionType : IOGraphType
{
    /// <summary>
    /// Represents the item type that is contained inside the collection.
    /// </summary>
    IOGraphType ItemType { get; }
}
