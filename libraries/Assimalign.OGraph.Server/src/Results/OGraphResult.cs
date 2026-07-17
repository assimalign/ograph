using System;
using System.Diagnostics.CodeAnalysis;

namespace Assimalign.OGraph;

/// <summary>
/// 
/// </summary>
public abstract class OGraphResult : IOGraphResult
{
    public OGraphResult() { }

#if NET7_0_OR_GREATER
    [SetsRequiredMembers]
#endif
    public OGraphResult(StatusCode statusCode)
    {
        StatusCode = statusCode;
    }
    /// <summary>
    /// 
    /// </summary>
    public virtual
#if NET7_0_OR_GREATER
    required
#endif
    StatusCode StatusCode { get; init; }


    #region Error Results

    public static IOGraphErrorResult Unauthorized()
    {
        return Unauthorized("The user is not authorized to access this resource.");
    }
    public static IOGraphErrorResult Unauthorized(string message)
    {
        return Unauthorized(error =>
        {
            error.Code = "Unauthorized";
            error.Message = message;
        });
    }
    public static IOGraphErrorResult Unauthorized(Action<OGraphError> configure) => throw new NotImplementedException();


    public static IOGraphErrorResult BadRequest()
    {
        return BadRequest("The request is invalid.");
    }
    public static IOGraphErrorResult BadRequest(string message)
    {
        return BadRequest(error =>
        {
            error.Code = "BadRequest";
            error.Message = message;
        });
    }
    public static IOGraphErrorResult BadRequest(Action<OGraphError> configure) => throw new NotImplementedException();



    #endregion
}
