using System.Security.Claims;
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
using Microsoft.AspNetCore.Http;
using API.Extensions;

namespace API.Controllers
{
   [Authorize]
    public class UsersController : BaseApiController
    {
        private readonly IUserRepository _userRepository ;
        private readonly IMapper _mapper;
        private readonly IPhotoService _photoService;

        public UsersController(IUserRepository userRepository, IMapper mapper, IPhotoService photoService)
{
             _userRepository = userRepository;
             _mapper = mapper;
             _photoService = photoService;
        } 

[HttpPut]
public async Task<ActionResult> UpdateUser (MemberUpdateDto memberUpdateDto){
    var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    var user = await _userRepository.GetUserByUserNameAsync(username);
    _mapper.Map(memberUpdateDto, user);
    _userRepository.Update(user);
    if (await _userRepository.SaveAllAsync()){
        return NoContent();
    }
            return BadRequest("Failed to update user");
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

[HttpGet("{userName}", Name ="GetUser")]
public async Task<ActionResult<MemberDto>>GetUserByUserName(string userName)
{
var member = await _userRepository.GetMemberAsync(userName);
// var user = await _userRepository.GetUserByUserNameAsync(userName);
// var member =  _mapper.Map<MemberDto>(User);
return Ok(member);
}

[HttpPost("add-photo")]
public async Task<ActionResult<PhotoDto>>addPhoto(IFormFile file)
{
     var username = User.GetUserName();
    var user = await _userRepository.GetUserByUserNameAsync(username);
    var result = await _photoService.UploadPhotoAsync(file);
    if (result.Error != null)
    {return BadRequest(result.Error.Message);}

    var photo = new Photo {
        Url = result.SecureUrl.AbsoluteUri,
        PublicId = result.PublicId
        };

        photo.IsMain = user.Photos.Count==0;
        _userRepository.Update(user);

        if (await _userRepository.SaveAllAsync())
        {
        //return _mapper.Map<PhotoDto>(photo);
        return CreatedAtRoute("GetUser", new {userName = user.UserName},  _mapper.Map<PhotoDto>(photo));
        }

        return BadRequest("problam adding photos");

    }
 
//  [HttpPut("set-main-photo/{photoId}")]
//     public async Task<ActionResult> SetMainPhoto(int photoId)
//     {
//         var username = User.GetUserName();
//         var user = await _userRepository.GetUserByUserNameAsync(username);
//         var photo 
//     }
     [HttpPut("delete-photo/{photoId}")]
    public async Task<ActionResult> deletePhoto(int photoId)
    {
        var username = User.GetUserName();
        var user = await _userRepository.GetUserByUserNameAsync(username);
        var photo =  user.Photos.FirstOrDefault(p=>p.Id == photoId);
        if (photo==null)
        {return BadRequest("The photo not exist");}
        if(photo.IsMain)
        {return BadRequest("You can't delete main photo");}
        if(photo.PublicId != null)
        {var result =  await _photoService.DeletePhotoAsync(photo.PublicId);
        if (result.Error != null) {return BadRequest("Same error occor");}
        return Ok(result);
        }
        return BadRequest("The photo not in cloudinery");
        

    }
}
}