using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Execution;

public sealed class OGraphExecutorOptions
{
}


public enum OnErrorExecutionStrategy
{
    Partial,

}

public abstract class OGraphFilterProvider
{

    public Type QueryableType { get; }


}