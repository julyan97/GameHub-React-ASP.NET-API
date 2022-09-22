using GameHub.Logic.Services.Game;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GameHub.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IGameService gameService;

        public GameController(IGameService gameService)
        {
            this.gameService=gameService;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            return Ok(gameService
                .GetAll()
                .Select(x => x.GameName));
        }
    }
}
