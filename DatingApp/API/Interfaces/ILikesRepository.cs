
using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.helpers;

namespace API.Interfaces
{
    public interface ILikesRepository
    {
        Task<UserLike> GetUserLike(int SourceUserId, int LikedUserId);

        Task<AppUser> GetUserWithLikes (int userId);
        Task<PageList<LikeDto>> GetUserLikes (LikesParams likesParams);
    }
}