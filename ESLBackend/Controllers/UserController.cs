using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ESLBackend.Utils;

namespace ESLBackend.Controllers
{
    // Allow anonymous access
    [AllowAnonymous] 
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        //private readonly SignInManager<IdentityUser> _signInManager;
        //private readonly UserManager<IdentityUser> _userManager;
        //private readonly ITokenService _tokenService; // Define ITokenService interface

        //public UserController(
        //    SignInManager<IdentityUser> signInManager,
        //    UserManager<IdentityUser> userManager,
        //    ITokenService tokenService) // Inject ITokenService
        //{
        //    _signInManager = signInManager;
        //    _userManager = userManager;
        //    _tokenService = tokenService;
        //}

        //[HttpPost("/login")]
        //public async Task<IActionResult> Login(UserLogin model) // Define LoginViewModel
        //{
        //    var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
        //    if (result.Succeeded)
        //    {
        //        // User successfully authenticated, issue bearer token
        //        var user = await _userManager.FindByEmailAsync(model.Email);
        //        var token = await _tokenService.GenerateJwtTokenAsync(user);

        //        return Ok(new { Token = token });
        //    }
        //    else
        //    {
        //        // Authentication failed, return error
        //        return BadRequest("Invalid login attempt.");
        //    }
        //}
    }
}
