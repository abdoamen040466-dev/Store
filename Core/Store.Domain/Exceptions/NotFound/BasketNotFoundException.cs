namespace Store.Domain.Exceptions.NotFound;

public class BasketNotFoundException(string id) : NotFoundException($"Basket with ID {id} not found.")
{
}
