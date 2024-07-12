using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Assimalign.OGraph.Gdm;

/// <summary>
/// 
/// </summary>
public interface IOGraphGdmMetadata : IDictionary<Label, object>
{
}


public interface IOGraphGdmMetadataValue : IXmlSerializable
{

}