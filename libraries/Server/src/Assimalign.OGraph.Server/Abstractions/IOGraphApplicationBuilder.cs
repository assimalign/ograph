using System;

namespace Assimalign.OGraph;

/// <summary>
/// 
/// </summary>
public interface IOGraphApplicationBuilder
{
    IOGraphApplicationBuilder Bind<T>(Action<IOGraphApplicationOperationDescriptor<T>> configure) 
        where T : class, new();
}