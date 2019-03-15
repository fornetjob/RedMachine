using Assets.Scripts.Features.Pooling;
using System;
using System.Collections.Generic;

public class ComponentProperty<T> : IPoolItem
    where T : IComponent, new()
{
    private IPool
        _parentPool;

    private List<IComponentListener<T>>
        _listeners = new List<IComponentListener<T>>();

    #region Properties

    public int id;
    public T value;

    #endregion

    public ComponentProperty<T> AddListener(IComponentListener<T> listener)
    {
        _listeners.Add(listener);

        listener.OnChanged(value);

        return this;
    }

    public void RemoveListener(IComponentListener<T> listener)
    {
        _listeners.Remove(listener);
    }

    public void Destroy()
    {
        if (value is IDestroy)
        {
            ((IDestroy)value).Destroy();
        }

        value = default(T);

        _listeners.Clear();

        _parentPool.Destroy(this);
    }

    public void Set(Action<T> action)
    {
        action(value);

        Set(value);
    }

    public void Set(T newValue)
    {
        value = newValue;

        for (int i = 0; i < _listeners.Count; i++)
        {
            _listeners[i].OnChanged(newValue);
        }
    }

    void IPoolItem.Attach(IPool pool)
    {
        _parentPool = pool;
    }

    int IPoolItem.Id
    {
        get { return id; }
        set { id = value; }
    }
}