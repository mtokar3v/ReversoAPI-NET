using System;
using System.Collections.Concurrent;
using System.Threading;

public class SimpleCache<TKey, TValue> : IDisposable, ICache<TKey, TValue>
{
    private static TimeSpan _defaultCacheExpireTime = TimeSpan.FromSeconds(120);
    private static TimeSpan _defaultItemExpireTime = TimeSpan.FromSeconds(120);

    private readonly ConcurrentDictionary<TKey, CacheItem<TValue>> _cache;

    private Timer _timer;
    private object _tLock = new object();
    private bool _disposed;

    public SimpleCache()
    {
        _cache = new ConcurrentDictionary<TKey, CacheItem<TValue>>();
        _disposed = false;

        RestartTimer(_defaultCacheExpireTime);
    }

    public void Add(TKey key, TValue value, TimeSpan? expireTime = null)
    {
        if (expireTime == null) expireTime = _defaultItemExpireTime;
        if (_disposed) RestartTimer(_defaultCacheExpireTime);

        var item = new CacheItem<TValue>(value, DateTime.Now.Add(expireTime.Value));
        _cache.AddOrUpdate(key, item, (k, v) => 
        {
            if(v is IDisposable disposable)
                disposable.Dispose();
            return item;
        });
    }

    public bool TryGetValue(TKey key, out TValue value)
    {
        if (_disposed) RestartTimer(_defaultCacheExpireTime);

        if (_cache.TryGetValue(key, out CacheItem<TValue> item) && !item.IsExpired())
        {
            value = item.Value;
            return true;
        }

        value = default;
        return false;
    }

    public TValue GetOrAdd(TKey key, Func<TValue> valueFactory, TimeSpan? expireTime = null)
    {
        if (expireTime == null) expireTime = _defaultItemExpireTime;
        if (_disposed) RestartTimer(_defaultCacheExpireTime);
        return _cache.GetOrAdd(key, value => new CacheItem<TValue>(valueFactory(), DateTime.Now.Add(expireTime.Value))).Value;
    }


    public void Dispose()
    {
        _timer.Dispose();
        _cache.Clear();
    }

    private void DisposeCache(object state)
    {
        foreach (var key in _cache.Keys)
        {
            if (_cache.TryGetValue(key, out CacheItem<TValue> item) && item.IsExpired())
            {
                if(_cache.TryRemove(key, out var value))
                {
                    if (value is IDisposable disposable)
                        disposable.Dispose();
                }
            }
        }

        if (_disposed)
        {
            _cache.Clear();
        }
        else if (_cache.IsEmpty)
        {
            Dispose();
        }
        else
        {
            _timer.Change(_defaultCacheExpireTime, TimeSpan.FromMilliseconds(-1));
        }
    }

    private void RestartTimer(TimeSpan expiry)
    {
        lock (_tLock)
        {
            _timer = new Timer(DisposeCache, null, expiry, TimeSpan.FromMilliseconds(-1));
        }
    }

    private class CacheItem<T>
    {
        private readonly DateTime _expiryTime;

        public CacheItem(T value, DateTime expiryTime)
        {
            Value = value;
            _expiryTime = expiryTime;
        }

        public T Value { get; }

        public bool IsExpired()
        {
            return DateTime.Now >= _expiryTime;
        }
    }
}