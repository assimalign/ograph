namespace Assimalign.OGraph;

using Assimalign.OGraph.Gdm;

public interface IOGraphOperationBindingDescriptor
{
    IOGraphOperationBindingDescriptor UseRoute(Route route);
    IOGraphOperationBindingDescriptor UseMethod(Method method);
    IOGraphOperationBindingDescriptor UseRequestType<TGdmType>() where TGdmType : IOGraphGdmType;
    IOGraphOperationBindingDescriptor UseMiddleware<TMiddleware>() where TMiddleware : IOGraphOperationBindingMiddleware, new();
    IOGraphOperationBindingDescriptor UseMiddleware(OGraphOperationBindingMiddleware middleware);
    IOGraphOperationBindingDescriptor UseMiddleware(IOGraphOperationBindingMiddleware middleware);
    IOGraphOperationBindingDescriptor UseResolver<TResolver>() where TResolver : IOGraphOperationBindingResolver, new();
    IOGraphOperationBindingDescriptor UseResolver(OGraphOperationBindingResolver resolver);
    IOGraphOperationBindingDescriptor UseResolver(IOGraphOperationBindingResolver resolver);
}