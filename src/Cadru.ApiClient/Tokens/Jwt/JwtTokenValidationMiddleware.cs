using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;

namespace Cadru.ApiClient.Tokens.Jwt
{
public class JwtTokenValidationMiddleware
{

    private readonly RequestDelegate _next;

    public JwtTokenValidationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(' ').Last();

        if (token != null)
        {
            // Validate the token
            ValidateJwtToken(token);

            // Validate the token using signing keys retrieved
            // from the OIDC provider.
            var signingKeys = await GetSigningKeysAsync();
            ValidateJwtToken(token, signingKeys);
        }

        await _next(context);
    }

    private async Task<ICollection<SecurityKey>> GetSigningKeysAsync()
    {
        var configurationManager = new ConfigurationManager<OpenIdConnectConfiguration>(
            "https://auth.pingone.com/5a473c78-29c6-4ce3-ac6a-d41dd13e4fe8/as/.well-known/openid-configuration",
            new OpenIdConnectConfigurationRetriever(),
            new HttpDocumentRetriever());

        var discoveryDocument = await configurationManager.GetConfigurationAsync();
        return discoveryDocument.SigningKeys;
    }

    private static ClaimsPrincipal? ValidateJwtToken(string token, IEnumerable<SecurityKey> signingKeys)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var validationParameters = new TokenValidationParameters
        {
            ClockSkew = TimeSpan.FromMinutes(5),
            IssuerSigningKeys = signingKeys,
            RequireSignedTokens = true,
            RequireExpirationTime = true,
            ValidateLifetime = true,
            ValidateAudience = true,
            ValidAudience = "api://default",
            ValidateIssuer = true,
            ValidIssuer = "https://localhost:5000/"
        };

        try
        {
            var claimsPrincipal = tokenHandler.ValidateToken(token, validationParameters, out var rawValidatedToken);
            return claimsPrincipal;
        }
        catch (SecurityTokenValidationException e)
        {
            // The token failed validation!
            // TODO: Log it or display an error.
            throw new Exception($"Token failed validation: {e.Message}");
        }
        catch (ArgumentException e)
        {
            // The token was not well-formed or was invalid for some other reason.
            // TODO: Log it or display an error.
            throw new Exception($"Token was invalid: {e.Message}");
        }
    }

    private static ClaimsPrincipal? ValidateJwtToken(string token)
    {
        var key = Encoding.ASCII.GetBytes("[SECRET USED TO SIGN AND VERIFY JWT TOKENS, IT CAN BE ANY STRING]");
        var tokenHandler = new JwtSecurityTokenHandler();
        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false,
            ClockSkew = TimeSpan.Zero
        };

        try
        {
            var claimsPrincipal = tokenHandler.ValidateToken(token, validationParameters, out var rawValidatedToken);
            return claimsPrincipal;
        }
        catch (SecurityTokenValidationException e)
        {
            // The token failed validation!
            // TODO: Log it or display an error.
            throw new Exception($"Token failed validation: {e.Message}");
        }
        catch (ArgumentException e)
        {
            // The token was not well-formed or was invalid for some other reason.
            // TODO: Log it or display an error.
            throw new Exception($"Token was invalid: {e.Message}");
        }
    }
}
}
