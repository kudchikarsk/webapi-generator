namespace WebApplication.Interfaces
{
    public interface IEntity<TId> 
    {
        TId Id { get; }
    }
}
