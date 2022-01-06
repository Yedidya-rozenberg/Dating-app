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
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly DataContext _dataContext;

public UsersController(DataContext context)
{
    _dataContext = context;
}         

[HttpGet]
[AllowAnonymous]
public async Task< ActionResult<IEnumerable<string>>>GetUsers()
{
    var users = await _dataContext.AppUsers.ToListAsync();
    var userNames = new List<string>();
        foreach (var user in users)
        {
            userNames.Add(user.UserName);
        }
     
    return userNames;
}

[Authorize]
[HttpGet("{id}")]
public async Task<ActionResult<string>>GetUserByID(int ID)
{
var user = await _dataContext.AppUsers.FindAsync(ID);
return user.UserName;
}




    }
}