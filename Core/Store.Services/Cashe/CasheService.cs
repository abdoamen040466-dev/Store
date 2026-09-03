using Store.Domain.Contracts;
using Store.Services.Abstractions.Cashe;

namespace Store.Services.Cashe;

public class CasheService(ICasheRepository _cashRepository) : ICasheService
{
    public async Task<string?> GetAsync(string key)
    {
        var result = await _cashRepository.GetAsync(key);
        return result;
    }

    public async Task SetAsync(string key, object value, TimeSpan duration)
    {
        await _cashRepository.SetAsync(key, value, duration);
    }
}
