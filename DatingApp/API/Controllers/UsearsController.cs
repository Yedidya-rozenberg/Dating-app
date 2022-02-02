using System.Collections;
using System.Runtime.InteropServices.ComTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Interfaces;
using AutoMapper;
using API.DTOs;

namespace API.Controllers
{
   [Authorize]
    public class UsersController : BaseApiController
    {
        private readonly IUserRepository _userRepository ;
        private readonly IMapper _mapper;

        public UsersController(IUserRepository userRepository, IMapper mapper)
{
             _userRepository = userRepository;
             _mapper = mapper;
        }         

[HttpGet]
public async Task< ActionResult<IEnumerable<MemberDto>>>GetUsers()
{
    var UserToTerutn = await _userRepository.GetMembersAsync();
    // var users = await _userRepository.GetUsersAsync();
    // var UserToTerutn = _mapper.Map<IEnumerable<MemberDto>>(users);
    return Ok(UserToTerutn);     
}

// [HttpGet("{id}")]
// public async Task<ActionResult<AppUser>>GetUserByID(int ID)
// {
// var user = await _userRepository.GetUserByIdAsync(ID);
// return user;
// }

[HttpGet("{userName}")]
public async Task<ActionResult<MemberDto>>GetUserByUserName(string userName)
{
var member = await _userRepository.GetMemberAsync(userName);
// var user = await _userRepository.GetUserByUserNameAsync(userName);
// var member =  _mapper.Map<MemberDto>(User);
return Ok(member);
}



    }
}