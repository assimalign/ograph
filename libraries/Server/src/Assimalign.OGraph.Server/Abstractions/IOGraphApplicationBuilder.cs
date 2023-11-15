using System;

namespace Assimalign.OGraph;

public interface IOGraphApplicationBuilder
{
    IOGraphApplicationBuilder Bind<T>(Action<IOGraphApplicationOperationDescriptor<T>> configure) where T : class, new();
}