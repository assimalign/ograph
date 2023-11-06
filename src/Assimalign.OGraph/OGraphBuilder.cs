using Assimalign.OGraph.Gdm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

public sealed class OGraphBuilder : IOGraphBuilder
{
    private readonly Label domain;
    private readonly IList<Action<IOGraphGdmBuilder>> onConfigureModel;

    public OGraphBuilder(Label domain)
    {
        this.domain = domain;
        onConfigureModel = new List<Action<IOGraphGdmBuilder>>();
    }


    public IOGraph Build()
    {
        var gdmBuilder = OGraphGdmBuilder.Create(domain);

        foreach (var action in onConfigureModel)
        {
            action.Invoke(gdmBuilder);
        }



        return new Internal.OGraph()
        {
            Model = gdmBuilder.Build()
        };
    }

    public IOGraphBuilder ConfigureApplication(Action<IOGraphApplicationBuilder> configure)
    {
        throw new NotImplementedException();
    }

    public IOGraphBuilder ConfigureModel(Action<IOGraphGdmBuilder> configure)
    {
        if (configure is null)
        {
            throw new ArgumentNullException(nameof(configure));
        }
        onConfigureModel.Add(configure);
        return this;
    }
}
