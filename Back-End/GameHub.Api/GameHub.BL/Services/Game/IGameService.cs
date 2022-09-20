namespace GameHub.Logic.Services.Game
{
    public interface IGameService
    {
        Common.Entities.Game GetByName(string name);
    }
}