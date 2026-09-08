namespace Store.Domain.Exceptions.NotFound;

public class DeliveryMethodNotFoundException(int id) : NotFoundException($"Delivery method with ID {id} was not found.")
{
}
