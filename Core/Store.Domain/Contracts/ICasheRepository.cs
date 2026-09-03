namespace Store.Domain.Contracts;

public interface ICasheRepository
{
    Task<string?> GetAsync(string key);
    Task SetAsync(string key, object value, TimeSpan duration);
}
