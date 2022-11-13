using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

public sealed class OGraphModelBuilder
{


    public OGraphModelBuilder AddEntity<T>()
    {

        return this;
    }

    public OGraphModelBuilder AddEntity<T>(string label)
    {

        return this;
    }

    public OGraphModelBuilder AddEntity<T>(Action<OGraphModelEntityBuilder<T>> configure)
    {


        return this;
    }

    public OGraphModelBuilder AddEdge()
    {
        return this;
    }
}
