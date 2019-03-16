public class ComponentBase : IComponent
{
    #region Fields

    private IPool
        _parentPool;

    private bool
        _isChanged;

    #endregion

    #region IComponent implementation

    public void Destroy()
    {
        OnDestroy();

        _parentPool.Destroy(this);
    }

    public int Id { get; set; }

    bool IComponent.IsChanged { get { return _isChanged; }  set { _isChanged = value; } }

    void IComponent.Attach(IPool pool)
    {
        _parentPool = pool;
    }

    #endregion

    #region Protected methods

    protected void MarkAsChanged()
    {
        if (Id == -1
            || _isChanged)
        {
            return;
        }

        _parentPool.OnChanged(Id);

        _isChanged = true;
    }

    #endregion

    #region Virtual methods

    protected virtual void OnDestroy()
    {

    }

    #endregion
}