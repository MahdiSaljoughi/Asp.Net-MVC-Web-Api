using MvcApi.Dto;

namespace MvcApi.Services.Interfaces;

public interface IAuthService
{
    public Task<ResponseDto> RegisterAsync(RegisterDto registerDto);
    
    public Task<ResponseDto> LoginAsync(LoginDto loginDto);
}