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


namespace API.Controllers
{
   [Authorize]
    public class UsersController : BaseApiController
    {
        private readonly DataContext _dataContext;

public UsersController(DataContext context)
{
    _dataContext = context;
}         

[HttpGet]
public async Task< ActionResult<IEnumerable<string>>>GetUsers()
{
    var users = await _dataContext.Users.ToListAsync();
    var userNames = new List<string>();
        foreach (var user in users)
        {
            userNames.Add(user.UserName);
        }
     
    return userNames;
}

[HttpGet("{id}")]
public async Task<ActionResult<AppUser>>GetUserByID(int ID)
{
var user = await _dataContext.Users.FindAsync(ID);
return user;
}




    }
}