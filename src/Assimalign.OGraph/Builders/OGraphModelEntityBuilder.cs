using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

public class OGraphModelEntityBuilder<T>
{

    public OGraphModelEntityBuilder<T> HasLabel(string label)
    {
        return this;
    }
}
