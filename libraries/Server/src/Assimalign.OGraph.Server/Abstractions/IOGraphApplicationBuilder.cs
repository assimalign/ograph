using System;

namespace Assimalign.OGraph;

/// <summary>
/// 
/// </summary>
public interface IOGraphApplicationBuilder
{
    IOGraphApplicationBuilder Bind<T>(Action<IOGraphApplicationOperationDescriptor> configure) 
        where T : class, new();
}