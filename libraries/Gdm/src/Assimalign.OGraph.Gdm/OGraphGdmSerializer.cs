using Assimalign.OGraph.Gdm.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm;

/// <summary>
/// 
/// </summary>
public abstract class OGraphGdmSerializer
{

    protected OGraphGdmSerializer(OGraphGdmSerializerOptions options)
    {
        Options = options;
    }

    /// <summary>
    /// 
    /// </summary>
    protected OGraphGdmSerializerOptions Options { get; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="stream"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    public abstract Task<IOGraphGdm> DeserializeAsync(Stream stream, CancellationToken cancellationToken = default );

    /// <summary>
    /// 
    /// </summary>
    /// <param name="stream"></param>
    /// <param name="model"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public abstract Task SerializeAsync(Stream stream, IOGraphGdm model, CancellationToken cancellationToken = default);


    /// <summary>
    /// 
    /// </summary>
    /// <param name="stream"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    public static IOGraphGdm Deserialize(Stream stream, OGraphGdmSerializerOptions? options = default)
    {
        return DeserializeAsync(stream, options: options).GetAwaiter().GetResult();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="stream"></param>
    /// <param name="options"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static Task<IOGraphGdm> DeserializeAsync(Stream stream, OGraphGdmSerializerOptions? options = default, CancellationToken cancellationToken = default)
    {
        options ??= new OGraphGdmSerializerOptions();

        var serializer = options.Version switch
        {
            OGraphVersion.Version1 => new GdmVersion1Serializer(options),

            _ => throw new Exception("Invalid version")
        };

        return serializer.DeserializeAsync(stream, cancellationToken);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="stream"></param>
    /// <param name="model"></param>
    /// <param name="options"></param>
    public static void Serialize(Stream stream, IOGraphGdm model, OGraphGdmSerializerOptions? options = default)
    {
        SerializeAsync(stream, model, options: options).GetAwaiter().GetResult();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="stream"></param>
    /// <param name="model"></param>
    /// <param name="options"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static Task SerializeAsync(Stream stream, IOGraphGdm model, OGraphGdmSerializerOptions? options = default, CancellationToken cancellationToken = default)
    {
        options ??= new OGraphGdmSerializerOptions();

        var serializer = options.Version switch
        {
            OGraphVersion.Version1 => new GdmVersion1Serializer(options),

            _ => throw new Exception("Invalid version")
        };

        return serializer.SerializeAsync(stream, model, cancellationToken);
    }
}