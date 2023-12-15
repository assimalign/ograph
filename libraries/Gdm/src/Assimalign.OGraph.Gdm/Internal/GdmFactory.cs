using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm.Internal;

internal class GdmFactory : IOGraphGdmFactory
{
    private readonly IEnumerable<IOGraphGdm> models;

    public GdmFactory(IEnumerable<IOGraphGdm> models)
    {
        this.models = models;
    }

    public IOGraphGdm Create(Label label)
    {
        var model = models.FirstOrDefault(p => p.Label == label);

        if (model is null)
        {
            GdmThrowHelper.ThrowArgumentException("");
        }

        return model!;        
    }
}
