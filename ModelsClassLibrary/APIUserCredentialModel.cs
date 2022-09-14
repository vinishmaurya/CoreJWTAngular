using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModelsClassLibrary
{
    public class APIUserCredentialModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string GrantType { get => _GrantType; set => _GrantType = value; }
        public string RefreshToken { get => _RefreshToken; set => _RefreshToken = value; }

        private string _GrantType;

        private string _RefreshToken;
        
    }
    public class RefreshTokenModel
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }

    public class TokenInfo
    {
        public string AccessToken { get; set; }
        public string TokenGeneratedDatetime { get; set; }
        public string ExpiryDatetime { get; set; }
        public RefreshTokenInfo RefreshTokenInfo { get; set; }
    }
    public class RefreshTokenInfo
    {
        public string RefreshToken { get; set; }
        public string ExpiryDatetime { get; set; }
    }
}
