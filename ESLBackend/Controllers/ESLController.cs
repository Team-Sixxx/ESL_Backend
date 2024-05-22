using Microsoft.AspNetCore.Authorization;
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
using ESLBackend.models;


namespace ESLBackend.Controllers
{
    
    [Route("[controller]")]
    public class ESLController : ControllerBase
    {


        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;


        public ESLController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;

        }







    }

}
