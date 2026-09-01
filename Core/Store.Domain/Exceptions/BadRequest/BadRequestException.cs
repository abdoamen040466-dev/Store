namespace Store.Domain.Exceptions.BadRequest;

public class BadRequestException(string message) : Exception(message)
{
}
