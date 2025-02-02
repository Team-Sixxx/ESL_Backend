﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Google;

using System.Security.Claims;
using Microsoft.AspNetCore.Cors;
using Microsoft.EntityFrameworkCore;
using Azure.Core;
using ESLBackend.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication;


namespace ESLBackend.Controllers
{
    
    [Route("[controller]")]
    public class UserController : ControllerBase
    {


        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;


        public UserController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;

        }


        [AllowAnonymous]
        [HttpGet("signin-google")]
        public async Task<IActionResult> SignInGoogle()
        {
            var state = Guid.NewGuid().ToString();
            HttpContext.Session.SetString("GoogleCsrfToken", state);

            var properties = new AuthenticationProperties
            {
                RedirectUri = Url.Action("GoogleSignInCallback", "User", null, Request.Scheme),
                Items = { { "state", state } }
            };

            await HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme, properties);

            var googleAuthUrl = HttpContext.Response.Headers["Location"].ToString();

            return Ok(new { GoogleAuthUrl = googleAuthUrl });
        }



        [AllowAnonymous]
        [HttpGet("google-signin-callback")]
        public async Task<IActionResult> GoogleSignInCallback()
        {
            try
            {
                var externalLoginInfo = await HttpContext.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);

                if (externalLoginInfo == null)
                    return BadRequest("External login failed.");

                if (!externalLoginInfo.Succeeded)
                    return BadRequest("External login failed.");

                var userEmail = externalLoginInfo.Principal?.FindFirstValue(ClaimTypes.Email);
                var userId = externalLoginInfo.Principal?.FindFirstValue(ClaimTypes.NameIdentifier);

                if (userEmail == null)
                    return BadRequest("External login failed.");

                var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == userEmail);
                if (user == null)
                {
                    return BadRequest("User not registered in Organization.");
                }

                // we sign in the user using cookie authentication
                var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Email, userEmail),
            new Claim(ClaimTypes.NameIdentifier, userId),

        };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    // authentication properties 
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                // session variable
                HttpContext.Session.SetString("IsAuthenticated", "true");


                if (!User.Identity.IsAuthenticated)
                {
                    return Ok();
                } else
                {

                    return Redirect(url: "http://localhost:5173/");

                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [AllowAnonymous]
        [Authorize]
        [HttpGet("profile")]
        public IActionResult GetProfile()
        {
            // Check if the user is authenticated using the cookies set inside callback
            //var userEmai1 = User.FindFirstValue(ClaimTypes.Email);
            if (!User.Identity.IsAuthenticated)
            {
                // Return 401 Unauthorized if not authenticated!
                return Unauthorized(); 
            }

            // way of access user claims
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            var name = User.FindFirstValue(ClaimTypes.Actor);

            return Ok(new {email = userEmail, name = name});
        }


        //testing endpoint
        [AllowAnonymous]
        [Authorize]
        [HttpGet("secure-endpoint")]
        public IActionResult SecureEndpoint()
        {
            // Check if the user is authenticated using cookies
            if (!User.Identity.IsAuthenticated)
            {
                return Unauthorized(); // Return 401 Unauthorized if not authenticated
            }

            // You can access user claims here if needed
            var userEmail = User.FindFirstValue(ClaimTypes.Email);

            return Ok("You are authorized to access this secure endpoint!");
        }


        private static readonly HttpClient client = new HttpClient();


        [AllowAnonymous]
        [HttpGet("get-token")]
        public async Task<IActionResult> GetToken()
        {

            if (!User.Identity.IsAuthenticated)
            {
                return Unauthorized(); // Return 401 Unauthorized if not authenticated
            }

            // You can access user claims here if needed
            var userEmail = User.FindFirstValue(ClaimTypes.Email);


            try
            {

                using StringContent jsonContent = new(
      JsonSerializer.Serialize(new
      {

          password = 12345678,
          username = "testing"

      }),
      Encoding.UTF8,
      "application/json");

                using HttpResponseMessage response = await client.PostAsync("http://162.62.125.25:5003/api/login", jsonContent);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                // Above three lines can be replaced with new helper method below
                // string responseBody = await client.GetStringAsync(uri);

                Console.WriteLine(responseBody);


                Token? token = JsonSerializer.Deserialize<Token>(responseBody);

                HttpContext.Session.SetString("token", token?.body);

                return Ok(new { Message = token?.body });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public class Token
        {


            public string body { get; set; }
            public string message { get; set; }




        }
    }

}
