using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
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
public async Task< ActionResult<IEnumerable<AppUser>>>GetUsers()
{
    var users = await _dataContext.AppUsers.ToListAsync();
    return users;
}

[HttpGet("{id}")]
public ActionResult<AppUser>GetUserByID(int ID)
{
var user = _dataContext.AppUsers.Find(ID);
return user;
}




    }
}