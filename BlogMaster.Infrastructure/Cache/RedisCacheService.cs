using System.Text.Json;
using StackExchange.Redis;

namespace BlogMaster.Infrastructure.Cache;

public class RedisCacheService:ICacheService
{
    private readonly IConnectionMultiplexer _redisCon;
    private readonly IDatabase _cache;
    private TimeSpan ExpireTime => TimeSpan.FromDays(1);

    public RedisCacheService(IConnectionMultiplexer redisCon)
    {
        _redisCon = redisCon;
        _cache = redisCon.GetDatabase();
    }

    public async Task Clear(string key)
    {
        await _cache.KeyDeleteAsync(key);
    }

    public void ClearAll()
    {
        var endpoints = _redisCon.GetEndPoints(true);
        foreach (var endpoint in endpoints)
        {
            var server = _redisCon.GetServer(endpoint);
            server.FlushAllDatabases();
        }
    }

    public async Task<T> GetOrAddAsync<T>(string key, Func<Task<T>> action) where T : class
    {
        var result = await _cache.StringGetAsync(key);
        if (result.IsNull)
        {
            result = JsonSerializer.SerializeToUtf8Bytes(await action());
            await SetValueAsync(key, result);
        }
        return JsonSerializer.Deserialize<T>(result);
    }

    public async Task<string> GetValueAsync(string key)
    {
        return await _cache.StringGetAsync(key);
    }

    public async Task<T?> GetValueAsync<T>(string key) where T: class
    {
        var result = await _cache.StringGetAsync(key);
        if (result.IsNull)
        {
            return await Task.FromResult<T>(null);
        }
        return JsonSerializer.Deserialize<T>(result);
    }

    public async Task AddAsync<T>(string key, T value)
    {
         await _cache.StringSetAsync(key,JsonSerializer.Serialize(value), ExpireTime);
    }

    public async Task<bool> SetValueAsync(string key, string value)
    {
        return await _cache.StringSetAsync(key,value, ExpireTime);
    }

    public T GetOrAdd<T>(string key, Func<T> action) where T : class
    {
        var result =  _cache.StringGet(key);
        if (result.IsNull)
        {
            result = JsonSerializer.SerializeToUtf8Bytes(action());
            _cache.StringSet(key, result,ExpireTime);
        }
        return JsonSerializer.Deserialize<T>(result);
    }
}
