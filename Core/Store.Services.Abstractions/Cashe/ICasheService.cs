namespace Store.Services.Abstractions.Cashe;

public interface ICasheService
{
    Task<string?> GetAsync(string key);
    Task SetAsync(string key, object value, TimeSpan duration);
}
