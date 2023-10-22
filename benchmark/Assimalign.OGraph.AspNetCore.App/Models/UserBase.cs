namespace Assimalign.OGraph;

public record class UserBase<T> : Entity<T>
    where T : Entity<T>
{
    public AuditField? Created { get; set; }
    public AuditField? Updated { get; set; }
}
