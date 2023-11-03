namespace Assimalign.OGraph;

public interface IOGraphApplication
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="operationName"></param>
    /// <param name="route"></param>
    /// <param name="resolver"></param>
    /// <returns></returns>
    IOGraphApplication MapGet<T>(Label operationName, Route route, OGraphOperationResolver resolver);
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="operationName"></param>
    /// <param name="route"></param>
    /// <param name="resolver"></param>
    /// <returns></returns>
    IOGraphApplication MapPut<T>(Label operationName, Route route, OGraphOperationResolver resolver);
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="operationName"></param>
    /// <param name="route"></param>
    /// <param name="resolver"></param>
    /// <returns></returns>
    IOGraphApplication MapPost<T>(Label operationName, Route route, OGraphOperationResolver resolver);
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="operationName"></param>
    /// <param name="route"></param>
    /// <param name="resolver"></param>
    /// <returns></returns>
    IOGraphApplication MapPatch<T>(Label operationName, Route route, OGraphOperationResolver resolver);
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="operationName"></param>
    /// <param name="route"></param>
    /// <param name="resolver"></param>
    /// <returns></returns>
    IOGraphApplication MapDelete<T>(Label operationName, Route route, OGraphOperationResolver resolver);
}
