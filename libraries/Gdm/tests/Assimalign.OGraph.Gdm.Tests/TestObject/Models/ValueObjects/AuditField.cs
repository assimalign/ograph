using System;

namespace Assimalign.OGraph.Gdm.Tests;

public record class AuditField
{
    public string? UserId { get; set; }
    public DateTimeOffset? Timestamp { get; set; }
}
