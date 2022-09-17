using GameHub.BL.Services.IServices;
using GameHub.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameHub.BL.Services
{
    public class BaseService : IBaseService
    {
        protected readonly IRepository repository;

        public BaseService(IRepository _repository)
        {
            repository = _repository;
        }

        public async Task SaveChangesAsync()
        {
            await repository.SaveChangesAsync();
        }
    }
}
