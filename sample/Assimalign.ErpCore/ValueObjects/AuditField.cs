using System;

namespace Assimalign.ErpCore;

public record class AuditField
{
    public string? User { get; set; }
    public DateTime? Timestamp { get; set; }
}
