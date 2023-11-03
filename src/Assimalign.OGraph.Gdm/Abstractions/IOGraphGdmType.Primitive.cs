using System;
using System.Xml;
using System.Text.Json;

namespace Assimalign.OGraph.Gdm;

public interface IOGraphGdmPrimitiveType : IOGraphGdmType
{
    /// <summary>
    /// Specifies allowed string formats.
    /// </summary>
    string[]? Formats { get; }
}