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
    //[TypeFilter(typeof(CustomAuthorizationFilter))]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IJwtAuth _jwtAuth;
        private readonly IConfiguration _configuration;

        private readonly List<Member> lstMember = new List<Member>()
        {
            new Member{Id=1, Name="Kirtesh" },
            new Member {Id=2, Name="Nitya" },
            new Member{Id=3, Name="pankaj"}
        };
        public AuthController(IJwtAuth jwtAuth, IConfiguration config)
        {
            _jwtAuth = jwtAuth;
            _configuration = config;
        }
       
         // GET: api/<MembersController>
        [HttpGet]
        public IEnumerable<Member> AllMembers()
        {
            return lstMember;
        }

        // GET api/<MembersController>/5
        [HttpGet("{id}")]
        public Member MemberByid(int id)
        {
            return lstMember.Find(x => x.Id == id);
        }

        [AllowAnonymous]
        // POST api/<MembersController>
        [HttpPost("token")]
        public IActionResult Token([FromBody] APIUserCredentialModel userCredential)
        {
            TokenInfo TokenInfo_OutObj = null;
            Messages objMessages = new Messages();
            TokenInfo_OutObj = _jwtAuth.Authentication(userCredential,out objMessages);
            ServiceResult<TokenInfo> objServiceResult = new ServiceResult<TokenInfo>();
            objServiceResult.Message = objMessages.Message;
            if (TokenInfo_OutObj != null && objMessages.Message_Id == 1)
            {
                objServiceResult.Data = TokenInfo_OutObj;
                objServiceResult.Result = true;
            }
            else
            {
                objServiceResult.Data = null;
                objServiceResult.Result = false;
            }
            return Ok(objServiceResult);
        }


        //[Authorize]
        //[HttpPost]
        //[Route("revokeRefreshToken/{username}")]
        //public async Task<IActionResult> Revoke(string username)
        //{
        //    var user = await _userManager.FindByNameAsync(username);
        //    if (user == null) return BadRequest("Invalid user name");

        //    user.RefreshToken = null;
        //    await _userManager.UpdateAsync(user);

        //    return NoContent();
        //}

        //[Authorize]
        //[HttpPost]
        //[Route("revokeAllRefreshToken")]
        //public async Task<IActionResult> RevokeAll()
        //{
        //    var users = _userManager.Users.ToList();
        //    foreach (var user in users)
        //    {
        //        user.RefreshToken = null;
        //        await _userManager.UpdateAsync(user);
        //    }

        //    return NoContent();
        //}

    }
}
