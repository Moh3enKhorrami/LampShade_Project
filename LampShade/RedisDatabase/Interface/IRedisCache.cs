namespace RedisDatabase.Infrastructure;

public interface IRedisCache
{
    T Get<T>(string key);
    void Remove(string key);
    bool KeyExist(string key);
    void Set(string key, Object value, int? expirationTimeInMinutes = null);
}