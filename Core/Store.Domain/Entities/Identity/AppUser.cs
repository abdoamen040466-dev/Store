using Microsoft.AspNetCore.Identity;

namespace Store.Domain.Entities.Identity;

public class AppUser : IdentityUser
{
    public string DisplyName { get; set; }
    public Address Address { get; set; }
}
