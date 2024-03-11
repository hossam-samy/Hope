using Hope.Core.Interfaces;
using Hope.Domain.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hope.Infrastructure.Repos
{
    public class BaseRepo<T>:IBaseRepo<T>where T : class
    {
        protected AppDBContext dbContext;

        public BaseRepo(AppDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async  Task AddAsync(T item)
        {

             
            await dbContext.AddAsync(item);
            
            await dbContext.SaveChangesAsync();  


            
        }

        public  async Task Delete(T item)
        {
            
            
            dbContext.Set<T>().Remove(item);
            await dbContext.SaveChangesAsync();

        }

        public Task<IEnumerable<TResult>> Get<TResult>(Func<T, bool> match, Func<T, TResult> selector )
        {
            
            return Task.FromResult(dbContext.Set<T>().Where(match).Select(selector));
        }

       
        public Task<IEnumerable<TResult>> Get<TResult>(Func<T, TResult> selector,string include)
        {
            return Task.FromResult(dbContext.Set<T>().Include(include).Select(selector));
        }
       
        public Task<IEnumerable<T>> Get(Func<T, bool> match)
        {
                return Task.FromResult(dbContext.Set<T>().Where(match));   
        }
        public Task<IEnumerable<T>> Get(Func<T, bool> match, string[] includes)
        {
            IQueryable<T> query = dbContext.Set<T>();
            if(includes is not null)
                foreach (var item in includes)
                {
                    query = query.Include(item);
                }
            return Task.FromResult(query.Where(match));
        }

        public Task<IEnumerable<TResult>> Get<TResult>(Func<T, TResult> selector)
        {
            return Task.FromResult<IEnumerable<TResult>>(dbContext.Set<T>().Select(selector).ToList());
        }

        //public async Task<IEnumerable<T>> GetAll()
        //{

        //   return dbContext.Set<T>().ToList();  
        //}

        public Task<IQueryable<T>> IgnoreFilter()
        {
            return Task.FromResult(dbContext.Set<T>().IgnoreQueryFilters());
        }

        public async Task<T> Update(T item)
        {
            dbContext.Set<T>().Update(item);
            await dbContext.SaveChangesAsync();    
            return item;
        }
    }
}
