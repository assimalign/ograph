using Assimalign.OGraph.Gdm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

public interface IOGraphOperation
{
}


public interface IOGraphOperationBindingDescriptor
{
    IOGraphOperationDescriptor MapGet(Label label);
}


public abstract class OperationBindings



public class Temp : IOGraphGdmBinding
{
    Task IOGraphGdmBinding.InvokeAsync(CancellationToken cancellationToken)
    {

        throw new NotImplementedException();
    }

    Task<object> IOGraphGdmBinding.InvokeAsync(object context, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }



    public Task<IOGraphResult> GetAsync()
    {
        return default;
    }
}