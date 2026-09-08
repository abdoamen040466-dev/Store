using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Store.Domain.Entities.Identity;
using Store.Services.Abstractions.Auth;
using Store.Services.Abstractions.Common;
using Store.Shared;
using Store.Shared.Dtos.Auth;
using Store.Shared.Dtos.Login;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Store.Services.Auth;

public class AuthService(UserManager<AppUser> _userManager, IOptions<JwtOptions> options) : IAuthService
{
    public async Task<Result<UserResponse>> LoginAsync(LoginRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user is null) return Error.Unauthorized(description: "Invalid email or password");

        var flag = await _userManager.CheckPasswordAsync(user, request.Password);
        if (!flag) return Error.Unauthorized(description: "Invalid email or password");

        return new UserResponse()
        {
            DisplyName = user.DisplyName,
            Email = user.Email,
            Token = await GenerateToken(user)
        };
    }

    public async Task<Result<UserResponse>> RegisterAsync(RegisterRequest request)
    {
        var user = new AppUser()
        {
            UserName = request.UserName,
            Email = request.Email,
            DisplyName = request.DisplayName,
            PhoneNumber = request.PhoneNumber
        };
        var result = await _userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
            return MapIdentityErrors(result.Errors);

        return new UserResponse()
        {
            DisplyName = user.DisplyName,
            Email = user.Email,
            Token = await GenerateToken(user)
        };

    }

    private List<Error> MapIdentityErrors(IEnumerable<IdentityError> errors)
    {
        var identityErrors = errors.ToList();

        List<Error> errorList = new List<Error>();

        if (identityErrors.Any(e =>
            e.Code is "DuplicateEmail" or "DuplicateUserName"))
        {
            var description = string.Join(
                ", ",
                identityErrors.Select(e => e.Description));

            var error = Error.Conflict(
                code: "Identity.Conflict",
                description: description);

            errorList.Add(error);
            return errorList;
        }

        foreach (var item in identityErrors)
        {
            var error = Error.Validation(
                code: item.Code,
                description: item.Description);
            errorList.Add(error);
        }
        return errorList;
    }

    private async Task<string> GenerateToken(AppUser user)
    {
        // TOKEN
        // 1. Header    (type, Algo)
        // 2. Payload   (Claims)
        // 3. Signature (Key)

        var authCliams = new List<Claim>()
        {
            new Claim(ClaimTypes.GivenName, user.DisplyName),
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.MobilePhone, user.PhoneNumber),

        };

        var roles = await _userManager.GetRolesAsync(user);

        foreach (var item in roles)
        {
            authCliams.Add(new Claim(ClaimTypes.Role, item));
        }

        var jwtOption = options.Value;

        // STRONGSECURITYKEYFORAUTHENTICATIONSTRONGSECURITYKEYFORAUTHENTICATIONSTRONGSECURITYKEYFORAUTHENTICATIONSTRONGSECURITYKEYFORAUTHENTICATION
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOption.SecretKey));

        var token = new JwtSecurityToken(
            issuer: jwtOption.Issure,
            audience: jwtOption.Audience,
            claims: authCliams,
            expires: DateTime.UtcNow.AddDays(double.Parse(jwtOption.DurationInDays)),
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)

            );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}

