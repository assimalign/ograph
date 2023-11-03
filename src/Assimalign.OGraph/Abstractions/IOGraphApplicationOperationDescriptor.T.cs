using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

public interface IOGraphApplicationOperationDescriptor<T>
{
    IOGraphApplicationQueryDescriptor<T> MapGet(Label operationName);
    IOGraphApplicationCommandDescriptor<T> MapPut(Label operationName);
    IOGraphApplicationCommandDescriptor<T> MapPost(Label operationName);
    IOGraphApplicationCommandDescriptor<T> MapPatch(Label operationName);
    IOGraphApplicationCommandDescriptor<T> MapDelete(Label operationName);
}
