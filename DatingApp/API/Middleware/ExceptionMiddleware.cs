using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace API.Middleware
{
    
    public class ExceptionMiddleware
    {
       private readonly RequestDelegate _next; 
       private readonly ILogger<ExceptionMiddleware> _logger;
       private readonly IHostEnvironment _evn;

       public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment evn )
       {
           this._next = next;
           this._logger = logger;
           this._evn = evn;
       }

       public async Task InvorkAsync(HttpContext context){
           try
           {
               await _next(context);
           }
           catch (Exception ex)
           {
                // TODO
           }
       }
    }
}