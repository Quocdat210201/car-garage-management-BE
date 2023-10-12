namespace Gara.Persistance.Abstractions
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangeAsync();
    }
}
