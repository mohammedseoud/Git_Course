using ElBayt.Common.Enums;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace ElBayt.Common.Security
{
    public interface IUserIdentity
    {
        ClaimsPrincipal User { get; }

        bool? IsAuthenticated { get; }

        string UserId { get; }

        string Email { get; }

        string Phone { get; }

        string UserName { get; }

        string Name { get; }

        string NormalizedName { get; }

        EnumUserCategory? UserCategory { get; }

        bool IsInRole(string roleName);


    }
}