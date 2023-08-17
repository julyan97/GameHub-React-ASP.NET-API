namespace GameHub.Logic.Services.User
{
    public interface IUserService
    {
        Common.Entities.User GetByName(string name);
        Common.Entities.User GetById(string id);
    }
}