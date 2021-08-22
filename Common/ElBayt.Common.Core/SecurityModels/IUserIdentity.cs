using ElBayt.Common.Enums;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace ElBayt.Common.Core.SecurityModels
{
    public class IUserIdentity
    { 
        public ClaimsPrincipal User { get; }

        public bool? IsAuthenticated { get; }

        public string UserId { get; }

        public string Email { get; }

        public string Phone { get; }

        public string UserName { get; }

        public string Name { get; }

        public string NormalizedName { get; }

        public EnumUserCategory? UserCategory { get; }

        //  public bool IsInRole(string roleName) { }

    }
}