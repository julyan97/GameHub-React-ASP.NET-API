using GameHub.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameHub.BL.Services.IServices
{
    public interface ICategoryService : IBaseService
    {
        public Task AddAsync(Category category);
        public Task DeleteAsync(Category category);
        public Task DeleteAsync(string id);
        public List<Category> FindAll();
        public Category FindByType(string type);
        public Category FindById(string id);
    }
}
