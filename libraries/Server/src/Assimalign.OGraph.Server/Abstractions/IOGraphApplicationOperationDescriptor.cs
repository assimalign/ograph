namespace Assimalign.OGraph;

public interface IOGraphApplicationOperationDescriptor
{
    IOGraphOperationBindingDescriptor MapQuery(Label label);
    IOGraphOperationBindingDescriptor MapCommand(Label label);





    IOGraphOperationBindingDescriptor MapGet(Label operationName);
    IOGraphOperationBindingDescriptor MapPut(Label operationName);
    IOGraphOperationBindingDescriptor MapPost(Label operationName);
    IOGraphOperationBindingDescriptor MapPatch(Label operationName);
    IOGraphOperationBindingDescriptor MapDelete(Label operationName);
}
