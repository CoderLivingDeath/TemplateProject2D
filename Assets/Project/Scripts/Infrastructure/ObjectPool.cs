using System;
using System.Collections.Generic;
using System.Threading;

public class ObjectPool<T>
{
    public List<PoolItem<T>> _pool;
    private readonly Func<T> _factory;
    private readonly Action<T> _onReturn;
    private readonly Action<T> _onGetting;
    private int _activeCount;
    private readonly object _lock = new object();

    public ObjectPool(int capacity = 0, Func<T> factory = null, Action<T> onGetting = null, Action<T> onReturn = null)
    {
        _pool = new List<PoolItem<T>>(capacity);
        this._factory = factory;
        this._onReturn = onReturn;
        this._onGetting = onGetting;
    }

    private bool ShouldCreate()
    {
        // Ищем свободный объект в пуле
        for (int i = 0; i < _pool.Count; i++)
        {
            if (!_pool[i].IsBusy)
            {
                return false; // Нашёл свободный
            }
        }
        // Все заняты или пул пуст — создаём новый
        return true;
    }

    private T CreateNew()
    {
        if (_factory == null)
        {
            throw new InvalidOperationException(
                $"Factory function is required to create new {typeof(T).Name}"
            );
        }
        return _factory();
    }

    private T GetFree()
    {
        for (int i = 0; i < _pool.Count; i++)
        {
            if (!_pool[i].IsBusy)
            {
                var item = _pool[i];
                item.IsBusy = true;
                _pool[i] = item;
                _activeCount++;

                return item.Value;
            }
        }

        var newInstance = CreateNew();
        CacheNewItem(newInstance);
        return newInstance;
    }

    public T Get()
    {
        lock (_lock)
        {
            if (ShouldCreate())
            {
                var newInstance = CreateNew();
                CacheNewItem(newInstance);
                _onGetting?.Invoke(newInstance);
                return newInstance;
            }
            else
            {
                var value = GetFree();
                _onGetting?.Invoke(value);
                return value;
            }
        }
    }

    private void CacheNewItem(T obj, bool prewarm = false)
    {
        int index = _pool.Count;
        var item = new PoolItem<T>(obj) { IsBusy = !prewarm };
        _pool.Add(item);
        
        if (!prewarm)
            _activeCount++;
    }

    public void Return(T obj)
    {
        if (obj == null)
        {
            throw new ArgumentNullException(nameof(obj));
        }

        lock (_lock)
        {
            for (int i = 0; i < _pool.Count; i++)
            {
                if (ReferenceEquals(_pool[i].Value, obj))
                {
                    if (_pool[i].IsBusy)
                    {
                        var item = _pool[i];
                        item.IsBusy = false;
                        _pool[i] = item;
                        _activeCount--;
                    }
                    _onReturn?.Invoke(obj);
                    return;
                }
            }

            throw new InvalidOperationException($"Object not found in pool: {typeof(T).Name}");
        }
    }

    public void Clear()
    {
        lock (_lock)
        {
            _pool.Clear();
            _activeCount = 0;
        }
    }

    public int FreeCount
    {
        get
        {
            int count = 0;
            for (int i = 0; i < _pool.Count; i++)
            {
                if (!_pool[i].IsBusy)
                    count++;
            }
            return count;
        }
    }

    public int ActiveCount => _activeCount;
    public int TotalCount => _pool.Count;
}
