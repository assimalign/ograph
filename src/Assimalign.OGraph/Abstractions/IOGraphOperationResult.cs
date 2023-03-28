using System;
using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

/// <summary>
/// 
/// </summary>
public interface IOGraphOperationResult
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="response"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task ExecuteAsync(IOGraphExecutorContext context, CancellationToken cancellationToken = default);
}


public interface IOGraphResult
{
    object Value { get; }
}

public record class OGraphOperationResult : Either<IOGraphResult, IOGraphError>
{
    public OGraphOperationResult(IOGraphError error) 
        : base(error) { }

    public OGraphOperationResult(IOGraphResult result) 
        : base(result) { }


    public OGraphOperationResult(OGraphOperationResult result) : base(result)
    {
        
    }

    public StatusCode StatusCode { get; set; }



    public static implicit operator OGraphOperationResult(IOGraphError value) =>
        new OGraphOperationResult(value);

    public static implicit operator OGraphOperationResult(IOGraphResult value) =>
        new OGraphOperationResult(value);

    //public static implicit operator Either<T1, T2>(Either<T2, T1> other) =>
    //    new Either<T1, T2>(other.valueType == 1 ? 2 : 1, other.value);
}


public record class OkOperationResult : OGraphOperationResult
{
    public OkOperationResult(IOGraphResult result) 
        : base(result)
    {
        StatusCode = 200;
    }
}



public class Demo
{
    public OGraphOperationResult Test(int value)
    {
        if ( value == 0)
        {
            return new OGraphError();
        }


        return default(IOGraphResult);

    }
}