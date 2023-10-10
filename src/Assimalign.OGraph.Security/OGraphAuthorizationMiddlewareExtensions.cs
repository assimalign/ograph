namespace Assimalign.OGraph.Security;

public static class OGraphAuthorizationMiddlewareExtensions
{
    public static IOGraphCommandOperationDescriptor UseAuthorization(this IOGraphCommandOperationDescriptor descriptor)
    {
        descriptor.UseMiddleware(async (context, next) =>
        {
            throw new UnauthorizedAccessException();
        });

        return descriptor;
    }
}