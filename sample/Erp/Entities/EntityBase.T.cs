namespace Erp;

public abstract class EntityBase<T> 
    where T : class, new()
{
    public Audit? Created { get; set; }
    public Audit? Updated { get; set; }
}
