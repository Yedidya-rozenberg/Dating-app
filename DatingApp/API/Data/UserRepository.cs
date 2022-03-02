using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.helpers;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class UserRepository : IUserRepository
    {
        public readonly DataContext _context;
        private readonly IMapper _mapper;

        public UserRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IEnumerable<AppUser>> GetUsersAsync()
        {
           return await _context.Users.Include(x=>x.Photos).ToListAsync();
        }

        public async Task<AppUser> GetUserByIdAsync(int id)
        {
           return await _context.Users.FindAsync(id);
        }

         public async Task<AppUser>GetUserByUserNameAsync(String userName)
        {
           return await _context.Users
           .Include(x=> x.Photos)
           .SingleOrDefaultAsync<AppUser>(x=>x.UserName==userName);
        }

        public async Task<bool> SaveAllAsync()
        {
            // var canges = _context.ChangeTracker.Entries<AppUser>().Count();
            // var numOfCanges =  await _context.SaveChangesAsync();
            // return canges == numOfCanges;
            return await _context.SaveChangesAsync()>0;
        }

        public void Update(AppUser user)
        {
            _context.Entry<AppUser>(user).State = EntityState.Modified;
        }

        public async Task<PageList<MemberDto>> GetMembersAsync(UserParams userParams)
        {
             var query  =  _context.Users
            .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
            .AsNoTracking();
            //add peremeters to query, do it and return.
            return await PageList<MemberDto>.CreateAsync(query,userParams.PageNumber, userParams.PageSize);
               
        }

        public async Task<MemberDto> GetMemberAsync(string userName)
        {
            return await _context.Users
            .Where(x=>x.UserName == userName)
            .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
            .SingleOrDefaultAsync();
        }

    }
}