using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

using Assimalign.OGraph.Gdm;

public interface IOGraphBuilder
{
    IOGraphBuilder ConfigureModel(Action<IOGraphGdmBuilder> configure);
    IOGraphBuilder ConfigureApplication(Action<IOGraphApplicationBuilder> configure);
    IOGraph Build();
}







