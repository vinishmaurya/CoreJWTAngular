using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsClassLibrary
{
    public class APIAuthMDL
    {
        private string _UserName;
        private string _Password;
        private string _GrantType;
        private int _RefreshTokenExpiryTimeInDays;
        private string _RefreshToken;
        public string UserName { get => _UserName; set => _UserName = value; }
        public string Password { get => _Password; set => _Password = value; }
        public string GrantType { get => _GrantType; set => _GrantType = value; }
        public int RefreshTokenExpiryTimeInDays { get => _RefreshTokenExpiryTimeInDays; set => _RefreshTokenExpiryTimeInDays = value; }
        public string RefreshToken { get => _RefreshToken; set => _RefreshToken = value; }
    }
}
