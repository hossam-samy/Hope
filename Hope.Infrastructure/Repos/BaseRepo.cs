﻿using Hope.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Hope.Infrastructure.Repos
{
    public class BaseRepo<T>:IBaseRepo<T>where T : class
    {
        protected AppDBContext dbContext;

        public BaseRepo(AppDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task AddAsync(T item)
        {
            Task.CompletedTask.Wait();  
            dbContext.Add(item);
            dbContext.SaveChanges();    
        }

        public async Task Delete(T item)
        {
            
            Task.CompletedTask.Wait();  
            dbContext.Set<T>().Remove(item);
            dbContext.SaveChanges();
        }

        public async Task<IEnumerable<TResult>> Get<TResult>(Func<T, bool> match, Func<T, TResult> selector )
        {
            return dbContext.Set<T>().Where(match).Select(selector);
        }
        public async Task<IEnumerable<TResult>> Get<TResult>( Func<T, TResult> selector)
        {
            var x = dbContext.Set<T>().Select(selector);
            return x;
        }
        public async Task<IEnumerable<TResult>> Get<TResult>(Func<T, TResult> selector,string include)
        {
            return dbContext.Set<T>().Include(include).Select(selector);
        }

        public async Task<IEnumerable<T>> Get(Func<T, bool> match)
        {
                return dbContext.Set<T>().Where(match);   
        }
        public async Task<IEnumerable<T>> Get(Func<T, bool> match, string[] includes)
        {
            IQueryable<T> query = dbContext.Set<T>();
            if(includes is not null)
                foreach (var item in includes)
                {
                    query = query.Include(item);
                }
            return query.Where(match);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
           return dbContext.Set<T>().ToList();  
        }

        public async Task<T> Update(T item)
        {
            dbContext.Set<T>().Update(item);
            dbContext.SaveChanges();    
            return item;
        }
    }
}
