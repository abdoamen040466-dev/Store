namespace Store.Domain.Exceptions.NotFound;

public class NotFoundException(string message) : Exception(message)
{
}
