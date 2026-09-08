namespace Store.Domain.Exceptions.NotFound;

public class OrderNotFoundException(Guid id) : NotFoundException($"Order with ID {id} not found.")
{
}
