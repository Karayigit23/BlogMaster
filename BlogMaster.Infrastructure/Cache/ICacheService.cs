namespace BlogMaster.Infrastructure.Cache;

public interface ICacheService
{
    
    Task<string> GetValueAsync(string key);
    Task<T?> GetValueAsync<T>(string key) where T: class;
    Task AddAsync<T>(string key,T value);
    Task<bool> SetValueAsync(string key, string value);
    Task<T> GetOrAddAsync<T>(string key, Func<Task<T>> action) where T : class;
    T GetOrAdd<T>(string key, Func<T> action) where T : class;
    Task Clear(string key);
    void ClearAll();
}