﻿using System.Security.Claims;

namespace DummyGram.Application.Extensions;

public static class ClaimExtensions
{
    public static string GetClaimValueByName(this IEnumerable<Claim> claims, string type)
    {
        return claims.Single(claim => claim.Type == type).Value;
    }
}