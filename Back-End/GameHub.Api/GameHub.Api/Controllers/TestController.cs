using GameHub.Common.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GameHub.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="Admin")]
    public class TestController : ControllerBase
    {
        private RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> userManager;

        public TestController(RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            _roleManager=roleManager;
            this.userManager=userManager;
        }
        [HttpGet("Test")]
        public async Task<IActionResult> Tester()
        {
            var userName = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var user = userManager.Users.FirstOrDefault(x => x.UserName == userName);
            return Ok(new
            {
                Roles = _roleManager.Roles,
                Roles2 = await userManager.GetRolesAsync(user),
                UserId = User.Claims.FirstOrDefault(x=>x.Type == "Id").Value,
                UserName = User.Identity.Name
            });
        }
    }
}
