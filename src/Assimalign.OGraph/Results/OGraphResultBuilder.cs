using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

public sealed class OGraphResultBuilder
{

    private readonly OGraphResult result;

    internal OGraphResultBuilder(OGraphResult result)
    {
        this.result = result;
    }




    public OGraphResultBuilder WithHeader(string header, HeaderValue headerValue)
    {


        return this;
    }






    public IOGraphOperationResult Build()
    {

        return result;
    }
}
