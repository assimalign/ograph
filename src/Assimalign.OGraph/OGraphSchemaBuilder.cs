using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

public class OGraphSchemaBuilder
{



    public OGraphSchemaBuilder AddEntity<T>()
    {
        return AddEntity<T>(typeof(T).Name);
    }

    public OGraphSchemaBuilder AddEntity<T>(string entityName)
    {

        return this;
    }


    public OGraphSchemaBuilder AddEntity<T>(Action<OGraphEntityBuilder<T>> configure)
    {
        

        retun this;
    }


}
