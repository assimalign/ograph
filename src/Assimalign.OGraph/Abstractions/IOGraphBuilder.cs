using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

public interface IOGraphBuilder
{
    IOGraphBuilder AddFormatter(string format, IOGraphContentFormatter formatter);
    IOGraphBuilder AddQuery(IOGraphQuery query);
    IOGraphBuilder AddQuery(string name, Action<IOGraphQueryDescriptor> descriptor);
    IOGraphBuilder AddCommand(IOGraphCommand command);
    IOGraphBuilder AddCommand(string name, Action<IOGraphCommandDescriptor> descriptor);

    IOGraph Build();
}
