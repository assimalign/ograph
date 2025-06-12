using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Internal;

using Assimalign.OGraph.Gdm;

internal class ApplicationOperationDescriptor<T> : IOGraphApplicationOperationDescriptor<T> where T : class, new()
{
    private readonly IOGraphGdmNode vertex;

    public ApplicationOperationDescriptor(IOGraphGdmNode vertex)
    {
        this.vertex = vertex;
    }

    public OGraphExecutorOptions Options { get; init; }

    public IOGraphOperationBindingDescriptor MapDelete(Label operationName)
    {
        var binding = new OperationBinding()
        {
            Label = operationName,
            Method = Method.Delete
        };

        vertex.Bind(binding);

        return new OperationBindingDescriptor(binding)
        {
            Options = this.Options
        };
    }

    public IOGraphOperationBindingDescriptor MapGet(Label operationName)
    {
        var binding = new OperationBinding()
        {
            Label = operationName,
            Method = Method.Get
        };

        vertex.Bind(binding);

        return new OperationBindingDescriptor(binding)
        {
            Options = this.Options
        };
    }

    public IOGraphOperationBindingDescriptor MapPatch(Label operationName)
    {
        var binding = new OperationBinding()
        {
            Label = operationName,
            Method = Method.Patch
        };

        vertex.Bind(binding);

        return new OperationBindingDescriptor(binding)
        {
            Options = this.Options
        };
    }

    public IOGraphOperationBindingDescriptor MapPost(Label operationName)
    {
        var binding = new OperationBinding()
        {
            Label = operationName,
            Method = Method.Post
        };

        vertex.Bind(binding);

        return new OperationBindingDescriptor(binding)
        {
            Options = this.Options
        };
    }

    public IOGraphOperationBindingDescriptor MapPut(Label operationName)
    {
        var binding = new OperationBinding()
        {
            Label = operationName,
            Method = Method.Put
        };

        vertex.Bind(binding);

        return new OperationBindingDescriptor(binding)
        {
            Options = this.Options
        };
    }
}
