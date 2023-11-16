using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm;

public sealed class OGraphGdmFactory : IOGraphGdmFactoryBuilder
{
    private readonly IList<IOGraphGdm> models;
    private readonly IDictionary<Label, IList<Action<IOGraphGdmBuilder>>> actions;

    public OGraphGdmFactory()
    {
        models = new List<IOGraphGdm>();
        actions = new Dictionary<Label, IList<Action<IOGraphGdmBuilder>>>();
    }

    public IOGraphGdmFactoryBuilder Configure(Label label, Action<IOGraphGdmBuilder> configure)
    {
        if (configure is null)
        {
            throw new ArgumentNullException(nameof(configure));
        }
        if (actions.TryGetValue(label, out var builds))
        {
            
        }
        else
        {

        }

        return this;
    }


    public IOGraphGdmFactory Build()
    {
        throw new NotImplementedException();
    }
}
