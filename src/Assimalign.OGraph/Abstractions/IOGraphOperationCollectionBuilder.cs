using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

public interface IOGraphOperationCollectionBuilder
{

    IOGraphOperationDescriptor AddOperation(Name name);




    IOGraphOperationCollection Build();
}
