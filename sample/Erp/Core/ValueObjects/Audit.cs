using System;

namespace Erp;

/// <summary>
/// 
/// </summary>
/// <param name="userId"></param>
/// <param name="timestamp">Defaults to DateTimeOffset.UtcNow</param>
public record Audit
{
    /// <summary>
    /// The user who updated or created the record.
    /// </summary>
    public UserId UserId { get; set; }
    /// <summary>
    /// The timestamp when the record was updated or created.
    /// </summary>
    public DateTimeOffset Timestamp { get; set; }
}
