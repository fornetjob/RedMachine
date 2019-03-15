using Assets.Scripts.Features.Pooling;
using System.Collections.Generic;

public class ComponentPool<T> : PoolBase<Component<T>>
    where T : IComponent, new()
{
    private List<IPoolListener<T>>
        _listeners = new List<IPoolListener<T>>();

    public ComponentPool<T> AddListener<TListener>(TListener listener)
        where TListener: IPoolListener<T>
    {
        _listeners.Add(listener);

        return this;
    }

    public void RemoveListener<TListener>(TListener listener)
        where TListener : IPoolListener<T>
    {
        _listeners.Remove(listener);
    }

    protected override void OnDestroy()
    {
        OnChange();
    }

    public Component<T> Create(int id, T value)
    {
        var item = Create(id);
        item.Set(value);

        OnChange();

        return item;
    }

    private void OnChange()
    {
        for (int i = 0; i < _listeners.Count; i++)
        {
            _listeners[i].OnChange(this);
        }
    }
}