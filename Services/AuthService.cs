using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using MvcApi.Dto;
using MvcApi.Models;
using MvcApi.Services.Interfaces;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace MvcApi.Services;

public class AuthService : IAuthService
{
    private readonly IUserService _userService;
    private readonly IConfiguration _configuration;

    public AuthService(IUserService userService, IConfiguration configuration)
    {
        _userService = userService;
        _configuration = configuration;
    }

    public async Task<ResponseDto> RegisterAsync(RegisterDto registerDto)
    {
        // Create OtpCode

        return new ResponseDto
        {
            Success = true, Message = "Send code successfully", Data = new { registerDto.Phone, code = 2020 },
            StatusCode = 200
        };
    }

    public async Task<ResponseDto> LoginAsync(LoginDto loginDto)
    {
        if (loginDto.Code != 2020)
        {
            return new ResponseDto
            {
                Success = false, Message = "Code is invalid", Data = new { loginDto },
                StatusCode = 400
            };
        }

        var user = await _userService.GetOneAsync(u => u.Phone == loginDto.Phone);

        if (user == null)
        {
            var result = await _userService.AddAsync(new User
            {
                Phone = loginDto.Phone,
                Role = "Customer"
            });

            if (!result.Success) return result;
            
            user = await _userService.GetOneAsync(u => u.Phone == loginDto.Phone);
        }

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user!.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(24),
            signingCredentials: creds
        );

        return new ResponseDto
        {
            Success = true, Message = "User login successfully",
            Data = new { token = new JwtSecurityTokenHandler().WriteToken(token), user },
            StatusCode = 200
        };
    }
}