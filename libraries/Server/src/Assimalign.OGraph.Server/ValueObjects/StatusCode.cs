using System;
using System.Net;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics;

namespace Assimalign.OGraph;

/// <summary>
/// Represents an HTTP Status Code.
/// </summary>
[DebuggerDisplay("{ToString()}")]
public readonly struct StatusCode :
    IEquatable<StatusCode>,
    IEqualityComparer<StatusCode>,
    IComparable<StatusCode>
{
    /// <summary>
    /// Represents the valid status supported by OGraph
    /// </summary>
    public static ReadOnlySpan<int> ValidStatusCodes => new int[]
    {
        200, // Ok
        201, // Created
        202, // Accepted
        204, // NotContent
        207, // MultiStatus
        400, // BadRequest
        401, // Unauthorized
        403, // Forbidden
        404, // NotFound
        405, // MethodNotAllowed
        406, // NotAcceptable
        408, // RequestTimeout
        409, // Conflict
        412, // PreconditionFailed
        414, // RequestUriTooLong
        415, // UnsupportedMediaType
        428, // PreconditionRequired
        429, // TooManyRequests
        500, // InternalServerError
        501, // NotImplemented
        502, // BadGateway
        503, // ServiceUnavailable
    };

    public StatusCode(int code)
    {
        if (!ValidStatusCodes.Contains(code))
        {
            throw new ArgumentOutOfRangeException(nameof(code), "The status code is not valid.");
        }
        Code = code;
    }

    /// <summary>
    /// The raw status code.
    /// </summary>
    public int Code { get; }

    /// <inheritdoc />
    bool IEquatable<StatusCode>.Equals(StatusCode statusCode)
    {
        return Code == statusCode.Code;
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
        return $"{Code} - {Enum.GetName(typeof(HttpStatusCode), (HttpStatusCode)Code)}";
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
    public static bool operator !=(StatusCode left, StatusCode right) => !left.Equals(right);
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
    public static StatusCode MultiStatus => new StatusCode(207);
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