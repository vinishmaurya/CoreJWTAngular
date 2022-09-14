using CoreJWTAngular.Models;
using ModelsClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CoreJWTAngular
{
    public interface IJwtAuth
    {
        TokenInfo Authentication(APIUserCredentialModel userCredential,out Messages objMessages);
    }
}
