using System;
using System.Net;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Assimalign.OGraph;

/// <summary>
/// Represents an HTTP Status Code.
/// </summary>
public readonly struct StatusCode : 
    IEquatable<StatusCode>, 
    IEqualityComparer<StatusCode>,
    IComparable<StatusCode>
{
    private static ReadOnlySpan<int> validStatusCodes => Enum.GetValues<HttpStatusCode>().Select(x => (int)x).ToArray();

    public StatusCode(int code)
    {
        if (!validStatusCodes.Contains(code))
        {
            throw new ArgumentOutOfRangeException(nameof(code), "The status code is not valid.");
        }
        this.Code = code;
    }

    /// <summary>
    /// 
    /// </summary>
    public int Code { get; }

    /// <inheritdoc />
    bool IEquatable<StatusCode>.Equals(StatusCode statusCode)
    {
        return this.Code == statusCode.Code;
    }

    /// <inheritdoc />
    bool IEqualityComparer<StatusCode>.Equals(StatusCode left, StatusCode right)
    {
        return left.Equals(right);
    }

    /// <inheritdoc />
    int IEqualityComparer<StatusCode>.GetHashCode(StatusCode statusCode)
    {
        return statusCode.GetHashCode();
    }

    /// <inheritdoc />
    int IComparable<StatusCode>.CompareTo(StatusCode statusCode)
    {
        return statusCode.Code.CompareTo(this.Code);
    }


    #region Overloads

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return Code.GetHashCode();
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return $"{Enum.GetName(typeof(HttpStatusCode), (HttpStatusCode)Code)} - {Code}";
    }

    /// <inheritdoc />
    public override bool Equals([NotNullWhen(true)] object? instance)
    {
        if (instance is StatusCode statusCode)
        {
            return Equals(statusCode);
        }
        return false;
    }

    #endregion

    #region Operators
    public static implicit operator StatusCode(int code) => new StatusCode(code);
    public static implicit operator int(StatusCode status) => status.Code;
    public static bool operator ==(StatusCode left, StatusCode right) => left.Equals(right);
    public static bool operator !=(StatusCode left, StatusCode right)=> !left.Equals(right);
    public static bool operator >(StatusCode left, StatusCode right) => ((IComparable<StatusCode>)right).CompareTo(left) > 0;
    public static bool operator <(StatusCode left, StatusCode right) => ((IComparable<StatusCode>)right).CompareTo(left) < 0;
    public static bool operator >=(StatusCode left, StatusCode right) => ((IComparable<StatusCode>)right).CompareTo(left) >= 0;
    public static bool operator <=(StatusCode left, StatusCode right) => ((IComparable<StatusCode>)right).CompareTo(left) <= 0;

    #endregion

    #region Success Status Codes
    public static StatusCode Ok => new StatusCode(200);
    public static StatusCode Created => new StatusCode(201);
    public static StatusCode Accepted => new StatusCode(202);
    public static StatusCode NotContent => new StatusCode(204);

    #endregion

    #region Bad Request Status Code
    public static StatusCode BadRequest => new StatusCode(400);
    public static StatusCode Unauthorized => new StatusCode(401);
    public static StatusCode Forbidden => new StatusCode(403);
    public static StatusCode NotFound => new StatusCode(404);
    public static StatusCode MethodNotAllowed => new StatusCode(405);
    public static StatusCode NotAcceptable => new StatusCode(406);
    public static StatusCode RequestTimeout => new StatusCode(408);
    public static StatusCode Conflict => new StatusCode(409);
    public static StatusCode PreconditionFailed => new StatusCode(412);
    public static StatusCode RequestUriTooLong => new StatusCode(414);
    public static StatusCode UnsupportedMediaType => new StatusCode(415);
    public static StatusCode PreconditionRequired => new StatusCode(428);
    public static StatusCode TooManyRequests => new StatusCode(429);

    #endregion

    #region Server Error Status Codes
    public static StatusCode InternalServerError => new StatusCode(500);
    public static StatusCode NotImplemented => new StatusCode(501);
    public static StatusCode BadGateway => new StatusCode(502);
    public static StatusCode ServiceUnavailable => new StatusCode(503);
    #endregion

}