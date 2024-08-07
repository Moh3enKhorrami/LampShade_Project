using System.Security.AccessControl;
using System.Text.Json;
using RedisDatabase.Infrastructure;
using StackExchange.Redis;


namespace Emplement.Infrastructure;

public class Rediscache : IRedisCache
{
    private readonly IConnectionMultiplexer _redis; // connection to Redis
    private readonly IDatabase _database; // Database Radis
    public Rediscache(IConnectionMultiplexer redis)
    {
        _redis = redis;
        _database = _redis.GetDatabase();
    }

    public T Get<T>(string key)
    {
        string value = _database.StringGet(key);
        if (value is null)
            return default;
        var obj = JsonSerializer.Deserialize<T>(value);
        if (obj is null)
            return default;
        return obj;

    }

    public void Remove(string key)
    {
        _database.KeyDelete(key);
    }

    public bool KeyExist(string key)
    {
        return _database.KeyExists(key);
    }

    public void Set(string key, object value, int? expirationTimeInMinutes = null)
    {
        var data = JsonSerializer.Serialize(value);
        _database.StringSet(
            key, 
            data, 
            expirationTimeInMinutes != null ? TimeSpan.FromMinutes(expirationTimeInMinutes.Value) : null );
    }
}