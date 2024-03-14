using Hope.Core.Interfaces;
using Hope.Infrastructure.Repos;
using System.Collections;

namespace Hope.Infrastructure
{
    public class UnitofWork:IUnitofWork
    {
        private readonly AppDBContext context;
        
       
       
        private Hashtable _repositries;



        public UnitofWork(AppDBContext context)
        {
            this.context = context;
        }
        public IBaseRepo<T> Repository<T>() where T : class
        {
            if (_repositries == null)
                _repositries = new Hashtable();

            var type = typeof(T).Name;

            if (!_repositries.ContainsKey(type))
            {
                var repositoryType = typeof(BaseRepo<>);

                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), context);

                _repositries.Add(type, repositoryInstance);
            }

            return (IBaseRepo<T>)_repositries[type]!;
        }
        public void Dispose()
        {

            context.Dispose();
        }

        public async Task<int> SaveAsync()
        {
           return await context.SaveChangesAsync();
        }

    }
}
        