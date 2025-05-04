using System;

namespace Assimalign.OGraph.Gdm.Tests.Objects;

public record class Audit
{
    public UserId? UserId { get; set; }
    public DateTimeOffset? Timestamp { get; set; }
}
