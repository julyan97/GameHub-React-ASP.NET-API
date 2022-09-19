using GameHub.BL.Services.IServices;
using GameHub.Common.Entities;
using GameHub.DAL.Repositories.Interfaces;

namespace GameHub.BL.Services
{
    public class CategoryService : BaseService, ICategoryService
    {
        public CategoryService(IRepository _repository) : base(_repository) { }

        public async Task AddAsync(Category category)
        {
            await repository.CreateAsync(category);
        }

        public async Task DeleteAsync(Category category)
        {
            await repository.DeleteAsync(category);
        }

        public async Task DeleteAsync(string id)
        {
            var category = repository.All<Category>(x => x.Id == id)
                .FirstOrDefault();
            await repository.DeleteAsync(category);
        }

        public List<Category> FindAll()
        {
            return repository.AllReadOnly<Category>()
                .ToList();
        }

        public Category FindById(string id)
        {
            return repository.AllReadOnly<Category>()
                .FirstOrDefault(x => x.Id == id);
        }

        public Category FindByType(string type)
        {
            return repository.AllReadOnly<Category>()
                .FirstOrDefault(x => x.Type == type);
        }

    }
}
