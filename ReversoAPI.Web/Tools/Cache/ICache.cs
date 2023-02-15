using System;

public interface ICache<TKey, TValue>
{
    void Add(TKey key, TValue value, TimeSpan? expireTime = null);
    TValue GetOrAdd(TKey key, Func<TValue> valueFactory, TimeSpan? expireTime = null);
    bool TryGetValue(TKey key, out TValue value);
    void Dispose();
}