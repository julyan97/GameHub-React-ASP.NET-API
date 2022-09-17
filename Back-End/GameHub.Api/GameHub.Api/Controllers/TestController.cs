using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GameHub.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TestController : ControllerBase
    {
        [HttpGet("Test")]
        public IActionResult Tester()
        {
            return Ok(new
            {
                Test = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value,
                IsAuth = User.Identity.IsAuthenticated,
                Key = User.Claims.ToList()[1].Type,
                Type = User.Claims.ToList()[1].ValueType,
                Name = User.Claims.ToList()[1].Value,
            }); ;
        }
    }
}
