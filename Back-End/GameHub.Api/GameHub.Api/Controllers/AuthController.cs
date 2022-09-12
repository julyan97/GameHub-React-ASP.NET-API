using GameHub.BL.Helpers;
using GameHub.Common.AuthModels.RequestModels;
using GameHub.Common.AuthModels.ResponseModels;
using GameHub.Common.GloballyNeededModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GameHub.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly JwtConfig _jwtConfig;

    public AuthController(UserManager<IdentityUser> userManage, IOptionsMonitor<JwtConfig> optionsMonitor)
    {
        this._userManager=userManage;
        _jwtConfig = optionsMonitor.CurrentValue;
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] UserRegistrationRequest user)
    {
        if (ModelState.IsValid)
        {
            var existingUser = await _userManager.FindByEmailAsync(user.Email);

            if (existingUser != null)
            {
                return BadRequest(new RegistrationResponse()
                {
                    Result = false,
                    Errors = new List<string>()
                        {
                            "Email already exist"
                        }

                });
            }

            var newUser = new IdentityUser() { Email = user.Email, UserName = user.Email };
            var isCreated = await _userManager.CreateAsync(newUser, user.Password);
            if (isCreated.Succeeded)
            {
                var jwtToken = JwtHelper.GenerateJwtToken(newUser, _jwtConfig);

                return Ok(new RegistrationResponse()
                {
                    Result = true,
                    Token = jwtToken
                });
            }

            return new JsonResult(new RegistrationResponse()
            {
                Result = false,
                Errors = isCreated.Errors.Select(x => x.Description).ToList()
            }
                    )
            { StatusCode = 500 };
        }

        return BadRequest(new RegistrationResponse()
        {
            Result = false,
            Errors = new List<string>()
                {
                    "Invalid payload"
                }
        });
    }


    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] UserLoginRequest user)
    {
        if (ModelState.IsValid)
        {
            //check if user with email exist
            var existingUser = await _userManager.FindByEmailAsync(user.Email);

            if (existingUser == null)
            {
                return BadRequest(new RegistrationResponse()
                {
                    Result = false,
                    Errors = new List<string>() {
                    "Irequest failed"
                    }

                });
            }

            var isCorrect = await _userManager.CheckPasswordAsync(existingUser, user.Password);

            if (isCorrect)
            {

                string jwtToken = JwtHelper.GenerateJwtToken(existingUser, _jwtConfig);

                return Ok(new RegistrationResponse()
                {
                    Result = true,
                    Token = jwtToken
                });
            }
            else
            {
                return BadRequest(new RegistrationResponse()
                {
                    Result = false,
                    Errors = new List<string>
                        {
                            "Invalid auth request"
                        }
                });
            }
        }

        return BadRequest(new RegistrationResponse()
        {
            Result = false,
            Errors = new List<string>()
                {
                    "Invalid payload"
                }
        });

    }

    [HttpPost("logout")]
    public IActionResult Logout()
    {
        Response.Cookies.Delete("jwt");

        return Ok(new
        {
            message = "success"
        });
    }
}

