namespace Assimalign.OGraph;

public interface IOGraphApplicationOperationDescriptor
{
    IOGraphOperationBindingDescriptor MapQuery(GdmLabel label);
    IOGraphOperationBindingDescriptor MapCommand(GdmLabel label);





    IOGraphOperationBindingDescriptor MapGet(GdmLabel operationName);
    IOGraphOperationBindingDescriptor MapPut(GdmLabel operationName);
    IOGraphOperationBindingDescriptor MapPost(GdmLabel operationName);
    IOGraphOperationBindingDescriptor MapPatch(GdmLabel operationName);
    IOGraphOperationBindingDescriptor MapDelete(GdmLabel operationName);
}
