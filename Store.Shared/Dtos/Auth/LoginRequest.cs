using System.ComponentModel.DataAnnotations;

namespace Store.Shared.Dtos.Login;

public class LoginRequest
{
    [EmailAddress]
    public string Email { get; set; }
    public string Password { get; set; }
}
