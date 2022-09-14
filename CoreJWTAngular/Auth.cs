using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
using DataContaxtClassLibrary;
using ModelsClassLibrary;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using CoreJWTAngular.Models;
using System.Collections.Generic;
using System.Linq;

namespace CoreJWTAngular
{
    public class Auth : IJwtAuth
    {
        APIAuthDAL objAPIAuthDAL = null;
        public IConfiguration _configuration { get; }

        public Auth(IConfiguration Configuration)
        {
            objAPIAuthDAL = new APIAuthDAL();
            this._configuration = Configuration;
        }
        public TokenInfo Authentication(APIUserCredentialModel userCredential,out Messages objMessages)
        {
            TokenInfo TokenInfo_OutObj = null;
            objMessages = null;
            try
            {
                int RefreshTokenExpiryTimeInDays = Convert.ToInt32(_configuration["JWT:RefreshTokenExpiryTimeInDays"]);
                APIAuthMDL objAPIAuthMDL = new APIAuthMDL()
                {
                    Password = userCredential.Password,
                    UserName = userCredential.UserName,
                    GrantType = userCredential.GrantType,
                    RefreshToken = userCredential.RefreshToken,
                    RefreshTokenExpiryTimeInDays = RefreshTokenExpiryTimeInDays
                };
                string Out_UserName = "";
                objMessages = objAPIAuthDAL.AuthenticateUser(objAPIAuthMDL,out Out_UserName);
                if (objMessages.Message_Id <= 0)
                {
                    return null;
                }

                // 1. Create Security Token Handler
                var tokenHandler = new JwtSecurityTokenHandler();

                // 2. Create Private Key to Encrypted
                var tokenKey = Encoding.ASCII.GetBytes(_configuration["JWT:SecretKey"]);

                //3. Create JETdescriptor
                var tokenDescriptor = new SecurityTokenDescriptor()
                {
                    Subject = new ClaimsIdentity(
                        new Claim[]
                        {
                        //new Claim(ClaimTypes.Name, userCredential.UserName),
                        //new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(ClaimTypes.Role, "APIAdmin")
                    }),
                    Expires = DateTime.Now.AddSeconds(Convert.ToInt32(_configuration["JWT:TokenExpiryTimeInSeconds"])),
                    SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
                };
                //4. Create Token

                var token = tokenHandler.CreateToken(tokenDescriptor);

                var refreshToken = GenerateRefreshToken();

                TokenInfo_OutObj = new TokenInfo()
                {
                    AccessToken = tokenHandler.WriteToken(token),
                    ExpiryDatetime = DateTime.Now.AddSeconds(Convert.ToInt32(_configuration["JWT:TokenExpiryTimeInSeconds"])).ToString("dd/MM/yyyy hh:mm:ss"),
                    TokenGeneratedDatetime = System.DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"),
                    RefreshTokenInfo = new RefreshTokenInfo()
                    {
                        RefreshToken = refreshToken,
                        ExpiryDatetime = DateTime.Now.AddDays(Convert.ToInt32(_configuration["JWT:RefreshTokenExpiryTimeInDays"])).ToString("dd/MM/yyyy hh:mm:ss")
                    }
                };

                objMessages = objAPIAuthDAL.SaveAuthenticatedUserDate(TokenInfo_OutObj, Out_UserName);
                if (objMessages.Message_Id <= 0)
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                var objBase = System.Reflection.MethodBase.GetCurrentMethod();
                LockErrors.SetError("Login Controller", "Account", "Login", "Login DAL > AuthenticateUser > Catch Block", ex.Message.ToString(), "Exception", "Exception During Login: " + ex.Message.ToString());
            }

            // 5. Return Token from method
            return TokenInfo_OutObj;
        }

        private static string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }

}
