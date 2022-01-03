using API.Entities;

namespace API.Interfaces
{
    public interface ITokenService
    {
         string CteateToken(AppUser user);
    }
}