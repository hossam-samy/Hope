namespace Hope.Core.Interfaces
{
    public interface IUnitofWork : IDisposable
    {
        IBaseRepo<T> Repository<T>() where T : class;

        Task<int> SaveAsync();
    }
}
