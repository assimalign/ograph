namespace Assimalign.OGraph;

public interface IOGraphApplicationOperationDescriptor<T> where T : class, new()
{
    IOGraphOperationBindingDescriptor MapGet(Label operationName);
    IOGraphOperationBindingDescriptor MapPut(Label operationName);
    IOGraphOperationBindingDescriptor MapPost(Label operationName);
    IOGraphOperationBindingDescriptor MapPatch(Label operationName);
    IOGraphOperationBindingDescriptor MapDelete(Label operationName);
}
