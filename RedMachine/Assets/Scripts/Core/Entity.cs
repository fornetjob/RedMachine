using Assets.Scripts.Features.Pooling;

public class Entity: IPoolItem, IAttachContext
{
    private IPool
        _parentPool;

    private PoolService
        _pool;

    public int id;

    public ComponentProperty<T> Add<T>(T value)
        where T : IComponent, new()
    {
        var pool = _pool.Provide<T>();

        var item = pool.Create(id);
        item.Set(value);

        return item;
    }

    public bool IsExist<T>()
        where T : IComponent, new()
    {
        var pool = _pool.Provide<T>();

        return pool.ContainsId(id);
    }

    public ComponentProperty<T> Get<T>()
        where T : IComponent, new()
    {
        var pool = _pool.Provide<T>();

        return pool.GetById(id);
    }

    public void Destroy()
    {
        _pool.DestroyAll(id);

        _parentPool.Destroy(this);
    }

    int IPoolItem.Id { get { return id; } set { id = value; } }

    void IPoolItem.Attach(IPool pool)
    {
        _parentPool = pool;
    }

    void IAttachContext.Attach(Context context)
    {
        _pool = context.services.pool;
    }
}