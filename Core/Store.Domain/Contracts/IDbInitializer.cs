namespace Store.Domain.Contracts;

public interface IDbInitializer
{
    Task InitializeAsync();
}
