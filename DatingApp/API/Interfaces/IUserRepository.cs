using System.Collections;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Entities;
using API.DTOs;
using API.helpers;

namespace API.Interfaces
{
    public interface IUserRepository
    {
         void Update(AppUser user);

         Task<bool> SaveAllAsync();

         Task<IEnumerable<AppUser>>GetUsersAsync();
         Task<AppUser>GetUserByIdAsync(int id);
         Task<AppUser>GetUserByUserNameAsync(String userName);

         Task<PageList<MemberDto>> GetMembersAsync(UserParams userParams);
         Task<MemberDto> GetMemberAsync(string userName);

    }
}