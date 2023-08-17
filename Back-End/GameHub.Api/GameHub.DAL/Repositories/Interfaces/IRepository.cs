using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GameHub.DAL.Repositories.Interfaces
{
        public interface IRepository
        {
            Task<T> UpdateAsync<T>(T data) where T : class;
            Task DeleteAsync<T>(T data) where T : class;
            Task DeleteRangeAsync<T>(params T[] data) where T : class;
            Task<T> CreateAsync<T>(T data) where T : class;
            Task<int> SaveChangesAsync();
            IQueryable<T> All<T>() where T : class;
            IQueryable<T> AllReadOnly<T>() where T : class;
            IQueryable<T> All<T>(Expression<Func<T, bool>> expression) where T : class;
            IQueryable<T> AllReadOnly<T>(Expression<Func<T, bool>> expression) where T : class;

        }
}
