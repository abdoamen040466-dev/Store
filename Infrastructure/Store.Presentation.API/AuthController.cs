using Microsoft.AspNetCore.Mvc;
using Store.Services.Abstractions;
using Store.Shared.Dtos.Auth;
using Store.Shared.Dtos.Login;

namespace Store.Presentation.API;

public class AuthController(IServiceManager _serviceManager) : APIBaseController
{
    // login
    [HttpPost("login")]
    public async Task<ActionResult<UserResponse>> Login(LoginRequest loginRequest)
    {
        var result = await _serviceManager.AuthService.LoginAsync(loginRequest);
        return HandleResult(result);
    }

    // register
    [HttpPost("register")]
    public async Task<ActionResult<UserResponse>> Register(RegisterRequest registerRequest)
    {
        var result = await _serviceManager.AuthService.RegisterAsync(registerRequest);
        return HandleResult(result);
    }
}
