using System;

namespace Erp;

public class Audit
{
    public UserId? UserId { get; set; }
    public DateTimeOffset? Timestamp { get; set; }
}
