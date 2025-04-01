using MvcApi.Extensions;
using MvcApi.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Extensions
builder.Services.ConfigureServices(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseMiddleware<RoleMiddleware>();
app.UseAuthorization();

app.UseStaticFiles();

app.MapControllers();

app.Run();