namespace Assimalign.OGraph.Security;

public static class OGraphAuthorizationMiddlewareExtensions
{
    public static IOGraphOperationDescriptor UseAuthorization(this IOGraphOperationDescriptor descriptor)
    {
        descriptor.UseMiddleware(async (context, next) =>
        {
            throw new UnauthorizedAccessException();
        });

        return descriptor;
    }
}