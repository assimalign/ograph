using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace Assimalign.OGraph.Gdm.Internal;

internal class GdmVersion1Serializer : GdmSerializer
{
    public GdmVersion1Serializer(GdmSerializerOptions options) : base(options)
    {
    }

    public override Task<IOGraphGdm> DeserializeAsync(Stream stream, CancellationToken cancellationToken = default)
    {
        var reader = XmlReader.Create(stream, new XmlReaderSettings()
        {
            IgnoreWhitespace = true,
            IgnoreComments = true,
            Async = true,
            
        });


        throw new NotImplementedException();
    }

   // private async Task ReadGraphAsync(XmlReader reader, Gdm)

    public override async Task SerializeAsync(Stream stream, IOGraphGdm model, CancellationToken cancellationToken = default)
    {
        var writer = XmlWriter.Create(stream, new XmlWriterSettings()
        {
            Async = true,
        });

        await writer.WriteStartDocumentAsync();
        await writer.WriteStartElementAsync(null, localName: GdmElementKey.Root, null);
        await writer.WriteAttributeStringAsync(null, "Version", null, "1.0");

        foreach (var graph in model.GetGdmGraphs())
        {
            await WriteGraphAsync(writer, graph);
        }

        await writer.WriteEndElementAsync();
        await writer.WriteEndDocumentAsync();
    }

    #region Writes


    private async Task WriteGraphAsync(XmlWriter writer, IOGraphGdmGraph graph)
    {
        await writer.WriteStartElementAsync(null, localName: GdmElementKey.Graph, null);


        await writer.WriteEndElementAsync();
    }

    #endregion
}
