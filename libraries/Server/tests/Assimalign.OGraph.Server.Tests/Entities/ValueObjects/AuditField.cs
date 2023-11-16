using System;

namespace Assimalign.OGraph.Server;

public record class AuditField
{
    public string? UserId { get; set; }
    public DateTimeOffset? Timestamp { get; set; }
}
