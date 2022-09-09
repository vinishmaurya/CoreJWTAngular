using CoreJWTAngular.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ModelsClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CoreJWTAngular.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IJwtAuth jwtAuth;
        private readonly IConfiguration configuration;
        public AuthController(IJwtAuth jwtAuth, IConfiguration config)
        {
            this.jwtAuth = jwtAuth;
            configuration = config;
        }
       

        [AllowAnonymous]
        // POST api/<MembersController>
        [HttpPost("authentication")]
        public IActionResult Authentication([FromBody] UserCredential userCredential)
        {
            string conString = configuration.GetConnectionString("DefaultConnection");
            string token = jwtAuth.Authentication(userCredential.UserName, userCredential.Password);

            ServiceResult<UserInfo> objServiceResult = new ServiceResult<UserInfo>();
            if (token != null)
            {
                objServiceResult.Data = new UserInfo() 
                { 
                    JWTToken=token,
                    ExpiryDatetime=System.DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"),
                    GeneratedDatetime = System.DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss") 
                };
                objServiceResult.Message = "Success!";
                objServiceResult.Result = true;
            }
            else
            {
                objServiceResult.Data = null;
                objServiceResult.Message = "Authentication Failed!";
                objServiceResult.Result = false;
            }
            return Ok(objServiceResult);
        }
    }
}
