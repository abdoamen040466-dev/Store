namespace Store.Domain.Exceptions.NotFound;

public class ProductNotFoundException(int id) : NotFoundException($"produst with id : {id} was not found !!")
{
}
