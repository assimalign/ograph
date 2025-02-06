using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace Assimalign.OGraph.Gdm.Internal;

internal class GdmVersion1Serializer : OGraphGdmSerializer
{
    public GdmVersion1Serializer(OGraphGdmSerializerOptions options) : base(options)
    {
    }

    public override Task<IOGraphGdm> DeserializeAsync(Stream stream, CancellationToken cancellationToken = default)
    {
        
        var reader = XmlReader.Create(stream, new XmlReaderSettings()
        {
            IgnoreWhitespace = true
        });


        throw new NotImplementedException();
    }

    public override async Task SerializeAsync(Stream stream, IOGraphGdm model, CancellationToken cancellationToken = default)
    {
        var writer = XmlWriter.Create(stream, new XmlWriterSettings()
        {
            
        });


        foreach (var graph in model.GetGdmGraphs())
        {
            await WriteGrpahAsync(writer, graph);
        }
    }

    #region Writes


    private async Task WriteGrpahAsync(XmlWriter writer, IOGraphGdmGraph graph)
    {
        await writer.WriteStartElementAsync(null, localName: GdmElementKey.Graph, null);


        await writer.WriteEndElementAsync();
    }

    #endregion
}
