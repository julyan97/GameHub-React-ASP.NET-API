using GameHub.Common.AuthModels.RequestModels;
using GameHub.Common.AuthModels.ResponseModels;
using GameHub.Common.Entities;
using GameHub.Common.GloballyNeededModels;
using GameHub.Logic.Helpers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace GameHub.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly JwtConfig _jwtConfig;

    public AuthController(UserManager<User> userManage, IOptionsMonitor<JwtConfig> optionsMonitor)
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

            //User Logic
            var newUser = new User() { Email = user.Email, UserName = user.Email, };
            var isCreated = await _userManager.CreateAsync(newUser, user.Password);

            //Role logic
            var isRoleCreated = await _userManager.AddToRoleAsync(newUser, "User");
            var roles = (await _userManager.GetRolesAsync(newUser));

            //Has User been created nad role been aded to use
            var HasSucceeded = isCreated.Succeeded && isRoleCreated.Succeeded;

            if (HasSucceeded)
            {
                var jwtToken = JwtHelper.GenerateJwtToken(newUser, _jwtConfig, String.Join(", ",roles));

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
            
            var roles = await _userManager.GetRolesAsync(existingUser);

            if (isCorrect)
            {

                string jwtToken = JwtHelper.GenerateJwtToken(existingUser, _jwtConfig,String.Join(", ",roles));
                Response.Cookies.Append("jwt", jwtToken);
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

    [HttpGet("Authenticate")]
    public IActionResult Authenticate()
    {
        string email = string.Empty;
        List<string> roles = new();
        if (User.Claims != null && User.Claims.Count() > 0)
        {
            var claims = User.Claims.ToList();
            email = claims[1].Value ?? string.Empty;
            roles = claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)
                .Value
                .Split(", ")
                .ToList();
        }


        return Ok(new
        {
            Id = User.Claims?.FirstOrDefault(x => x.Type == "Id")?.Value,
            Authenticated = User.Identity.IsAuthenticated,
            UserName = email,
            Roles = roles
        });
    }
}

