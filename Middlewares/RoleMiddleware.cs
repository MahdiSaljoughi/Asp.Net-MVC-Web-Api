using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using MvcApi.Data;

namespace MvcApi.Middlewares;

public class RoleMiddleware
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly RequestDelegate _next;

    public RoleMiddleware(IServiceScopeFactory scopeFactory, RequestDelegate next)
    {
        _scopeFactory = scopeFactory;
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        
        if (!string.IsNullOrEmpty(userId))
        {
            using var scope = _scopeFactory.CreateScope();
            var _context = scope.ServiceProvider.GetRequiredService<DataContext>();

            var user = await _context.Users
                .Where(u => u.Id.ToString() == userId)
                .Select(u => new { u.Role }) 
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);

            if (user?.Role != null)
            {
                var claimsIdentity = context.User.Identity as ClaimsIdentity;
                if (claimsIdentity != null)
                {
                    foreach (var claim in claimsIdentity.FindAll(ClaimTypes.Role))
                    {
                        claimsIdentity.RemoveClaim(claim);
                    }
                    
                    claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, user.Role.ToString()!));
                }
            }
        }

        await _next(context).ConfigureAwait(false);
    }
}