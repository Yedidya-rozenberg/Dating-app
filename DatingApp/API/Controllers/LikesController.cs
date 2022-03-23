
using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.helpers;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class LikesController : BaseApiController
    {
        private readonly IUserRepository _userRepository;
        private readonly ILikesRepository _likesRepository;

        public LikesController(IUserRepository userRepository, ILikesRepository likesRepository)
        {
            this._userRepository = userRepository;
            this._likesRepository = likesRepository;
        }
        [HttpPost("{username}")]
        public async Task<ActionResult> AddLike(string username)
        {
            var SourceUserId = User.GetUserId();
            var LikedUser = await _userRepository.GetUserByUserNameAsync(username);
            var SourceUser = await _likesRepository.GetUserWithLikes(SourceUserId);

            if (LikedUser == null) return NotFound();
            if (SourceUser.UserName==username) return BadRequest("You alredy like yourself.");

            var userLike = await _likesRepository.GetUserLike(SourceUserId, LikedUser.Id);
            if (userLike != null) return BadRequest("You like this user.");

            userLike = new UserLike{
                SourceUser = SourceUser,
                LikedUser = LikedUser
            };
            SourceUser.LikedUsers.Add(userLike);
            if(await _userRepository.SaveAllAsync()) return Ok();
            return BadRequest("Faild to like user.");
        }

        public async Task<ActionResult<IEnumerable<LikeDto>>> GetLikeUsers ([FromQuery]LikesParams likesParams)
        {
            likesParams.UserId = User.GetUserId();
            var users = await _likesRepository.GetUserLikes(likesParams);
            Response.AddPaginationHeader(users.CurrentPage, users.PageSize, users.TotalCount, users.TotalPages);
            return Ok(users);
        }
    }
}