using GameHub.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameHub.Logic.Services.User
{
    public class UserService : IUserService
    {
        private readonly IRepository repository;

        public UserService(IRepository repository)
        {
            this.repository=repository;
        }

        public Common.Entities.User GetByName(string name)
        {
            return repository
                .All<Common.Entities.User>(x => x.UserName == name)
                .FirstOrDefault();
        }

        public Common.Entities.User GetById(string id)
        {
            return repository
                .All<Common.Entities.User>(x => x.Id == id)
                .FirstOrDefault();
        }
    }
}
