using StackExchange.Redis;
using Store.Domain.Contracts;
using System.Text.Json;

namespace Store.Persistance.Repositories;

public class CasheRepository(IConnectionMultiplexer _connection) : ICasheRepository
{
    private readonly IDatabase _database = _connection.GetDatabase();
    public async Task<string?> GetAsync(string key)
    {
        var redisValue = await _database.StringGetAsync(key);
        return redisValue;
    }

    public async Task SetAsync(string key, object value, TimeSpan duration)
    {

        await _database.StringSetAsync(key, JsonSerializer.Serialize(value), duration);
    }
}
