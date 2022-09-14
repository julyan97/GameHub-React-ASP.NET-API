using GameHub.DAL.Data;
using GameHub.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GameHub.DAL.Repositories
{
    public class Repository : IRepository
    {
        protected GameHubDbContext context;

        public Repository(GameHubDbContext _context)
        {
            context = _context;
        }

        public IQueryable<T> All<T>() where T : class
        {
            return context.Set<T>();
        }

        public IQueryable<T> All<T>(Expression<Func<T, bool>> expression) where T : class
        {
            return context.Set<T>().Where(expression);
        }

        public IQueryable<T> AllReadOnly<T>() where T : class
        {
            return context.Set<T>().AsNoTracking();
        }

        public IQueryable<T> AllReadOnly<T>(Expression<Func<T, bool>> expression) where T : class
        {
            return context.Set<T>().Where(expression).AsNoTracking();
        }

        public async Task<T> CreateAsync<T>(T data) where T : class
        {
            await context.Set<T>().AddAsync(data);
            return data;
        }

        public async Task DeleteAsync<T>(T data) where T : class
        {
            context.Set<T>().Remove(data);
        }

        public async Task<T> UpdateAsync<T>(T data) where T : class
        {
            context.Set<T>().Update(data);
            return data;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await context.SaveChangesAsync(); // Catch later
        }

        public async Task DeleteRangeAsync<T>(params T[] data) where T : class
        {
            context.Set<T>().RemoveRange(data);
        }

    }
}
