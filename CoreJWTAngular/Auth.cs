using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
using DataContaxtClassLibrary;
using ModelsClassLibrary;

namespace CoreJWTAngular
{
    public class Auth : IJwtAuth
    {
        private readonly string key;
        APIAuthDAL objAPIAuthDAL = null;
        public Auth(string key)
        {
            objAPIAuthDAL = new APIAuthDAL();
            this.key = key;
        }
        public string Authentication(string username, string password)
        {
            APIAuthMDL objAPIAuthMDL = new APIAuthMDL() { Password = password, UserName = username };
            Messages objMessages = objAPIAuthDAL.AuthenticateUser(objAPIAuthMDL);
            if (objMessages.Message_Id <= 0)
            {
                return null;
            }

            // 1. Create Security Token Handler
            var tokenHandler = new JwtSecurityTokenHandler();

            // 2. Create Private Key to Encrypted
            var tokenKey = Encoding.ASCII.GetBytes(key);

            //3. Create JETdescriptor
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.Name, username)
                    }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            //4. Create Token
            var token = tokenHandler.CreateToken(tokenDescriptor);

            // 5. Return Token from method
            return tokenHandler.WriteToken(token);
        }
    }

}
