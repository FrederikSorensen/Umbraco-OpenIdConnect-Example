﻿using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Umbraco.Cms.Core.Security;

namespace Umbraco_OpenIdConnect_Example.Core.Extensions;

public static class MemberManagerExtensions
{
    public static MemberIdentityUser CreateVirtualUser(this IMemberManager memberManager, string id, IEnumerable<Claim> claims)
    {
        var user = new MemberIdentityUser
        {
            Id = id,
            UserName = claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value,
        };
        
        foreach(var claim in claims)
        {
            user.Claims.Add(new IdentityUserClaim<string>
            {
                ClaimType = claim.Type,
                ClaimValue = claim.Value
            });
        }
        user.IsApproved = true;
        
        var idToken = claims.FirstOrDefault(x => x.Type == "id_token")?.Value;
        if (!string.IsNullOrEmpty(idToken))
        {
            // TODO add id_token to the user.
        }

        return user;
    }
}