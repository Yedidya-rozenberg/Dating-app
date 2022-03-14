
using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
    public interface ILikesRepository
    {
        Task<UserLike> GetUserLike(int SourceUserId, int LikedUserId);

        Task<AppUser> GetUserLikes(int userId);
        Task<IEnumerable<LikeDto>> GetUserWithLikes(string predicate ,int userId);
    }
}