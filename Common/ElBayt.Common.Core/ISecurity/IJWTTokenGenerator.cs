using ElBayt.Common.Core.SecurityModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace ElBayt.Common.Core.ISecurity
{
    public interface IJWTTokenGenerator
    {
        string GenerateToken(IdentityUser user);
    }
}
