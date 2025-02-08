namespace Assimalign.OGraph.Gdm.Tests.Objects;

public interface IEntity<TSelf> 
    where TSelf : IEntity<TSelf>
{

}

public abstract class Entity<T> : IEntity<T> 
    where T : IEntity<T>
{
    public Audit? Created { get; set; }
    public Audit? Updated { get; set; }
}