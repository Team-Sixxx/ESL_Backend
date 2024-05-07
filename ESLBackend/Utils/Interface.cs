using Microsoft.AspNetCore.Identity;

namespace ESLBackend.Utils
{
    public interface ITokenService
    {
        Task<string> GenerateJwtTokenAsync(IdentityUser user);
    }
}
