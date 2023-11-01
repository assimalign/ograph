using Assimalign.OGraph.Gdm.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm;

public sealed class OGraphGdmBuilder : IOGraphGdmBuilder
{

    private OGraphGdmBuilder() { }

    IOGraphGdmBuilder IOGraphGdmBuilder.AddVertex<T>(Action<IOGraphGdmVertexDescriptor<T>> configure)
    {
        if (configure is null)
        {
            throw new ArgumentNullException(nameof(configure));
        }

        var vertex = new GdmVertex()
        {
            Label = typeof(T).Name
        };

        var descriptor = new GdmVertexDescriptor<T>(vertex);

        configure.Invoke(descriptor);

        return this;
    }

    IOGraphGdmBuilder IOGraphGdmBuilder.AddVertex(IOGraphGdmVertex vertex)
    {
        throw new NotImplementedException();
    }

    IOGraphGdmBuilder IOGraphGdmBuilder.AddVertex<TVertex>()
    {
        throw new NotImplementedException();
    }

    IOGraphGdmBuilder IOGraphGdmBuilder.AddType<T>(Label label, Action<IOGraphGdmComplexTypeDescriptor<T>> configure)
    {
        throw new NotImplementedException();
    }

    IOGraphGdm IOGraphGdmBuilder.Build()
    {
        throw new NotImplementedException();
    }

    #region Static Memebers

    /// <summary>
    /// 
    /// </summary>
    /// <param name="label">The Graph Model name.</param>
    /// <param name="configure"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static IOGraphGdm Create(Label label, Action<IOGraphGdmBuilder> configure)
    {
        if (configure is null)
        {
            throw new ArgumentNullException(nameof(configure));
        }

        IOGraphGdmBuilder builder = new OGraphGdmBuilder();

        configure.Invoke(builder);

        return builder.Build();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="label"></param>
    /// <returns></returns>
    public static IOGraphGdmBuilder Create(Label label)
    {
        return new OGraphGdmBuilder();
    }

    

    #endregion
}
