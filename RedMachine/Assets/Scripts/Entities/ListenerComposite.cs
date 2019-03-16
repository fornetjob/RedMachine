using System.Collections.Generic;

public class ListenerComposite<T>:IListener<T>
{
    private List<IListener<T>>
        _listeners = new List<IListener<T>>();

    public bool IsClear()
    {
        return _listeners.Count == 0;
    }

    public void AddListener(IListener<T> listener)
    {
        _listeners.Add(listener);
    }

    public void RemoveListener(IListener<T> listener)
    {
        _listeners.Remove(listener);
    }

    public void Clear()
    {
        _listeners.Clear();
    }

    public void OnChanged(T value)
    {
        for (int i = 0; i < _listeners.Count; i++)
        {
            _listeners[i].OnChanged(value);
        }
    }
}