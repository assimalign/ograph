using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm;

using Assimalign.OGraph.Gdm.Internal;

public sealed class OGraphGdmBuilder : IOGraphGdmBuilder
{
    private readonly Gdm model;
    private readonly GdmValidator validator;
    private readonly GdmBuilderContext context;
    private OGraphGdmBuilder(Label label)
    {
        this.validator = new();
        this.model = new()
        {
            Label = label
        };
        this.context = new(model);
    }

    IOGraphGdmBuilder IOGraphGdmBuilder.AddVertex<T>(Action<IOGraphGdmEntityTypeDescriptor<T>> configure)
    {
        return (this as IOGraphGdmBuilder).AddVertex(typeof(T).Name, configure);
    }
    IOGraphGdmBuilder IOGraphGdmBuilder.AddVertex<T>(Label label, Action<IOGraphGdmEntityTypeDescriptor<T>> configure)
    {
        if (configure is null)
        {
            throw new ArgumentNullException(nameof(configure));
        }
        var type = new GdmEntityType<T>();
        var vertex = new GdmVertex<GdmEntityType<T>>()
        {
            label = label,
            type = new GdmTypeReference()
            {
                Definition = type
            }
        };
        var descriptor = new GdmEntityTypeDescriptor<T>(type)
        {
            Context = new GdmBuilderContext(model)
        };

        configure.Invoke(descriptor);

        return (this as IOGraphGdmBuilder).AddVertex(vertex);
    }
    IOGraphGdmBuilder IOGraphGdmBuilder.AddVertex<TVertex>()
    {
        return (this as IOGraphGdmBuilder).AddVertex(new TVertex());
    }
    
    IOGraphGdmBuilder IOGraphGdmBuilder.AddVertex(Action<IOGraphGdmVertexDescriptor> configure)
    {
        return (this as IOGraphGdmBuilder).AddVertex(GdmVertex.Create(configure));
    }
    IOGraphGdmBuilder IOGraphGdmBuilder.AddVertex<T>(Action<IOGraphGdmVertexDescriptor<T>> configure)
    {
        return (this as IOGraphGdmBuilder).AddVertex(GdmVertex<T>.Create(configure));
    }
    IOGraphGdmBuilder IOGraphGdmBuilder.AddVertex(IOGraphGdmVertex vertex)
    {
        if (vertex is null)
        {
            throw new ArgumentNullException(nameof(vertex));
        }
        context.AddVertex(vertex);
        return this;
    }

    IOGraphGdm IOGraphGdmBuilder.Build()
    {


        var result = validator.Validate(model);

        if (!result.IsValid)
        {

        }

        return model;
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

        IOGraphGdmBuilder builder = new OGraphGdmBuilder(label);

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
        return new OGraphGdmBuilder(label);
    }

    #endregion
}
