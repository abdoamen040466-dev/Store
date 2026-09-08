namespace Store.Domain.Exceptions.NotFound;

public class UserNotFoundException(string email) : NotFoundException($"user with email: {email} was not found")
{
}
