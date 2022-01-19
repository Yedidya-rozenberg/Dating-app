using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
  
    public class buggyConrtoller : BaseApiController
    {
        private readonly DataContext _context;

        public buggyConrtoller(DataContext context)
        {
            this._context = context;
        }
        [Authorize]
        [HttpGet("auth")]
        public ActionResult<string> getSecret(){
            return "Secret string";
        }
        [HttpGet("not-found")]
        public ActionResult<AppUser> getNotFound(){
            var thing = _context.AppUsers.Find(-1);
            if (thing == null)
            {
                return NotFound();
            } 
            return Ok();
        }
                [HttpGet("server-error")]
        public ActionResult<string> getServerError(){
            var thing = _context.AppUsers.Find(-1);  
            var thingToString =  thing.ToString();//nullReferenceExeption
          return thingToString;
        }
[HttpGet("bad-request")]
        public ActionResult<string> getBedREquest(){
        return BadRequest("Servr error");
        }

    }
}