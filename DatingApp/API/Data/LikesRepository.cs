using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;
using API.Extensions;

namespace API.Data
{
    public class LikesRepository : ILikesRepository
    {
          private readonly DataContext _context;
        public LikesRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<UserLike> GetUserLike(int SourceUserId, int LikedUserId)
        {
            return await _context.userLikes.FindAsync(LikedUserId, SourceUserId);
        }

        public async Task<AppUser> GetUserWithLikes (int userId)
        {
            return await _context.Users
            .Include(u=>u.LikedUsers)
            .FirstOrDefaultAsync(u=>u.Id==userId);
        }

        public async Task<IEnumerable<LikeDto>> GetUserLikes (string predicate ,int userId)
        {
            IQueryable<AppUser> users;
            var likes = _context.userLikes.AsQueryable();
            if (predicate=="liked")
            {
                likes = likes.Where(like=>like.SourceUserId==userId);
                users = likes.Select(like=>like.LikedUser);
            }
            else
            {
                likes = likes.Where(like=>like.LikedUserId==userId);
                users = likes.Select(like=>like.SourceUser);
            }
            return await users.Select(user => new LikeDto{
                Age = user.DateOfBirth.CalculateAge(),
                City = user.City,
                Country = user.Country,
                Id = user.Id,
                KnownUs = user.KnownAs,
                PhotoUrl = user.Photos.FirstOrDefault(p=>p.IsMain).Url,
                UserName = user.UserName
            }).ToListAsync();
        }
    }
}