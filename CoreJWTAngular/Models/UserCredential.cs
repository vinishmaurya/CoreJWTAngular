using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreJWTAngular.Models
{
    public class UserCredential
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
    public class UserInfo
    {
        public string JWTToken { get; set; }
        public string ExpiryDatetime { get; set; }
        public string GeneratedDatetime { get; set; }
    }
}
