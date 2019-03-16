using Assets.Scripts.Features.Pooling;

public class Entity: ComponentBase, IAttachContext
{
    #region Services

    private PoolService
        _pool;

    #endregion

    #region IAttachContext implementation

    void IAttachContext.Attach(Context context)
    {
        _pool = context.services.pool;
    }

    #endregion

    #region Public methods

    public Entity AddListener<T>(params IListener<T>[] listeners)
        where T : IComponent, new()
    {
        var pool = _pool.Provide<T>();

        pool.AddListeners(Id, listeners);

        return this;
    }

    public T Add<T>()
        where T : IComponent, new()
    {
        var pool = _pool.Provide<T>();

        var item = pool.Create(Id);

        return item;
    }

    public bool IsExist<T>()
        where T : IComponent, new()
    {
        var pool = _pool.Provide<T>();

        return pool.ContainsId(Id);
    }

    public T Get<T>()
        where T : IComponent, new()
    {
        var pool = _pool.Provide<T>();

        return pool.GetById(Id);
    }

    #endregion

    #region Overriden methods

    protected override void OnDestroy()
    {
        _pool.DestroyAll(Id);
    }

    #endregion
}