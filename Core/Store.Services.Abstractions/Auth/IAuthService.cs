using Store.Services.Abstractions.Common;
using Store.Shared.Dtos.Auth;
using Store.Shared.Dtos.Login;

namespace Store.Services.Abstractions.Auth;

public interface IAuthService
{
    Task<Result<UserResponse>> LoginAsync(LoginRequest request);
    Task<Result<UserResponse>> RegisterAsync(RegisterRequest request);
}
