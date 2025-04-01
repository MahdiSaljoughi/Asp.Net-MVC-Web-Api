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
            using (var scope = _scopeFactory.CreateScope()) // ایجاد یک Scope جدید
            {
                var _context =
                    scope.ServiceProvider.GetRequiredService<DataContext>(); // گرفتن DataContext از Scope جدید

                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.Id.ToString() == userId); // حذف Include چون Role فقط یک string است

                if (user != null)
                {
                    var claimsIdentity = context.User.Identity as ClaimsIdentity;
                    if (claimsIdentity != null)
                    {
                        // حذف هر رول قدیمی قبل از اضافه کردن رول جدید
                        var existingRoleClaims = claimsIdentity.FindAll(ClaimTypes.Role).ToList();
                        foreach (var claim in existingRoleClaims)
                        {
                            claimsIdentity.RemoveClaim(claim);
                        }

                        // اضافه کردن نقش جدید
                        claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, user.Role));
                    }
                }
            }
        }

        await _next(context);
    }
}