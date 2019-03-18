using Assets.Scripts.Features.Identities;

using System.Collections.Generic;
using System.Linq;

public class ComponentPool<T> : IPool
    where T : IComponent, new()
{
    #region Services

    private IdentityService
        _identity;

    #endregion

    #region Fields

    private readonly Queue<T>
       _destroyed = new Queue<T>();

    private readonly Dictionary<int, T>
        _dict = new Dictionary<int, T>();

    private readonly List<int>
        _changedList = new List<int>();

    private readonly Dictionary<int, ListenerComposite<T>>
        _listeners = new Dictionary<int, ListenerComposite<T>>();

    private readonly Context
        _context;

    private readonly bool
        _isContextAttach;

    private readonly bool
        _isIdentity;

    #endregion

    #region ctor

    public ComponentPool(Context context)
    {
        _context = context;

        _identity = _context.services.identity;

        var type = typeof(T);

        _isContextAttach = typeof(IAttachContext).IsAssignableFrom(type);

        var componentAttribute = type.GetCustomAttributes(false).OfType<ComponentAttribute>().FirstOrDefault();

        if (componentAttribute != null)
        {
            _isIdentity = componentAttribute.IsIdentity;
        }
        else
        {
            _isIdentity = true;
        }
    }

    #endregion

    #region Events

    public readonly ListenerComposite<ComponentPool<T>> OnCountChanged = new ListenerComposite<ComponentPool<T>>();

    #endregion

    #region Public methods

    public void AddListeners(int id, params IListener<T>[] listeners)
    {
        ListenerComposite<T> listenerComposite;

        if (_listeners.TryGetValue(id, out listenerComposite) == false)
        {
            listenerComposite = new ListenerComposite<T>();

            _listeners.Add(id, listenerComposite);
        }

        for (int i = 0; i < listeners.Length; i++)
        {
            listenerComposite.AddListener(listeners[i]);
        }
    }

    public void RemoveListeners(int id, params IListener<T>[] listeners)
    {
        var listenerComposite = _listeners[id];

        for (int i = 0; i < listeners.Length; i++)
        {
            listenerComposite.RemoveListener(listeners[i]);
        }
    }

    public void ApplyChanges()
    {
        if (_changedList.Count == 0)
        {
            return;
        }

        for (int i = 0; i < _changedList.Count; i++)
        {
            var id = _changedList[i];

            // list changed
            if (id == int.MaxValue)
            {
                OnCountChanged.OnChanged(this);
            }
            else
            {
                ListenerComposite<T> listenerComposite;

                if (_listeners.TryGetValue(id, out listenerComposite))
                {
                    T item;

                    if (_dict.TryGetValue(id, out item))
                    {
                        listenerComposite.OnChanged(item);

                        item.IsChanged = false;
                    }
                }
            }
        }

        _changedList.Clear();
    }

    public T Single()
    {
        if (Items.Count != 1)
        {
            throw new System.ArgumentOutOfRangeException("count");
        }

        return Items[0];
    }

    public List<T> Items = new List<T>();

    public bool ContainsId(int id)
    {
        return _dict.ContainsKey(id);
    }

    public T GetById(int id)
    {
        return _dict[id];
    }

    public T Create()
    {
        return Create(-1);
    }

    public T Create(int id)
    {
        if (id == -1
            && _isIdentity)
        {
            id = _identity.NewId();
        }

        T item;

        if (_destroyed.Count == 0)
        {
            item = new T();

            if (_isContextAttach)
            {
                ((IAttachContext)item).Attach(_context);
            }
        }
        else
        {
            item = _destroyed.Dequeue();
        }

        item.Id = id;
        item.Attach(this);

        Items.Add(item);

        if (id != -1)
        {
            _dict.Add(id, item);
        }

        _changedList.Add(int.MaxValue);

        return item;
    }

    public void Destroy(int id)
    {
        GetById(id).Destroy();
    }

    public void DestroyAll()
    {
        while (Items.Count > 0)
        {
            Items[0].Destroy();
        }
    }

    #endregion

    #region IPool implementation

    void IPool.Destroy(IComponent item)
    {
        T itemToDestroy = (T)item;

        if (_listeners.ContainsKey(item.Id))
        {
            _listeners[item.Id].Clear();

            _listeners.Remove(item.Id);
        }

        _changedList.Remove(item.Id);

        Items.Remove(itemToDestroy);
        _dict.Remove(item.Id);

        _destroyed.Enqueue(itemToDestroy);

        _changedList.Add(int.MaxValue);
    }

    IComponent IPool.GetById(int id)
    {
        return _dict[id];
    }

    void IPool.OnChanged(int id)
    {
        _changedList.Add(id);
    }

    #endregion
}