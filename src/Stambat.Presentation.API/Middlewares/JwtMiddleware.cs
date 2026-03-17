using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

using Microsoft.IdentityModel.Tokens;

using Stambat.Domain.Exceptions;
using Stambat.Domain.Interfaces.Application.Services;

namespace Stambat.WebAPI.Middlewares;

/// <summary>
/// Middleware for validating JWT tokens and attaching user information to the HTTP context.
/// </summary>
/// <remarks>
/// Make sure to update the configuration settings for "Jwt:JwtSecretKey", "Jwt:JwtIssuer", and "Jwt:JwtAudience" as needed.
/// Visit https://jwtsecret.com/generate for generating a secure JWT secret key.
/// </remarks>
public class JwtMiddleware(
    RequestDelegate next,
    IHttpContextAccessor httpContextAccessor,
    ILogger<JwtMiddleware> logger)
{
    private readonly RequestDelegate _next = next;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    private readonly ILogger<JwtMiddleware> _logger = logger;

    /// <summary>
    /// Invokes the middleware to validate the JWT token from the request header and attach the user to the context.
    /// </summary>
    /// <param name="context">The current HTTP context.</param>
    /// <returns>A <see cref="Task"/> that represents the asynchronous operation.</returns>
    public async Task Invoke(HttpContext context, ICurrentUserService currentUser, ITenantProviderService currentTenant)
    {
        IJwtService _jwtService = context.RequestServices.GetRequiredService<IJwtService>();

        // 1. Always try to get the Tenant ID from the header first, regardless of the Token status
        string? tenantIdHeader = context.Request.Headers["X-Tenant-Id"].ToString();
        if (!string.IsNullOrEmpty(tenantIdHeader) && Guid.TryParse(tenantIdHeader, out var tId))
        {
            currentTenant.TenantId = tId;
        }

        string? token = context.Request.Headers["Authorization"]
            .FirstOrDefault()?.Split(" ").Last();

        if (string.IsNullOrEmpty(token))
        {
            await _next(context);
            return;
        }

        try
        {
            ClaimsPrincipal? principal = await _jwtService.ValidateToken(token!);
            if (principal is not null)
            {
                context.User = principal;


                // Try the short alias ('nameid') which is the standard JWT name for the ID.
                string? userId = principal.FindFirst(JwtRegisteredClaimNames.NameId)?.Value
                                 // Fallback to other common aliases if needed
                                 ?? principal.FindFirst("sub")?.Value
                                 ?? principal.FindFirst("id")?.Value;

                // If you want to keep the .NET URI constant, ensure you check that too:
                userId ??= principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                string? tenantId = _httpContextAccessor.HttpContext?.Request.Headers["X-Tenant-Id"].ToString();

                if (!string.IsNullOrEmpty(userId))
                {
                    currentUser.UserId = Guid.Parse(userId);
                }

                if (!string.IsNullOrEmpty(tenantId))
                {
                    currentTenant.TenantId = Guid.Parse(tenantId);
                }
            }
        }
        catch (UnauthenticatedException ex)
        {
            // Token is expired but we can still extract claims for refresh flow
            if (ex.Message.Contains("Invalid token") || ex.InnerException is SecurityTokenExpiredException)
            {
                try
                {
                    // Read token without validation to extract claims
                    var handler = new JwtSecurityTokenHandler();
                    var jwtToken = handler.ReadJwtToken(token);

                    string? userId = jwtToken.Claims.FirstOrDefault(c => c.Type == "sub")?.Value
                                     ?? jwtToken.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.NameId)?.Value
                                     ?? jwtToken.Claims.FirstOrDefault(c => c.Type == "id")?.Value
                                     ?? jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

                    if (!string.IsNullOrEmpty(userId))
                    {
                        currentUser.UserId = Guid.Parse(userId);
                        _logger.LogDebug("Extracted userId from expired token for refresh flow");
                    }
                }
                catch (Exception readEx)
                {
                    _logger.LogWarning(readEx, "Failed to extract claims from invalid token");
                }
            }
            else
            {
                _logger.LogWarning(ex, "Authentication failed");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to validate JWT token");
        }
        await _next(context);
    }
}
