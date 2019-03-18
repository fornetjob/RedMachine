using UnityEngine;

public class ComponentBase : IComponent
{
    #region Fields

    private IPool
        _parentPool;

    private bool
        _isChanged;

    [SerializeField]
    private int
        _id;

    #endregion

    #region IComponent implementation

    public void Destroy()
    {
        OnDestroy();

        _parentPool.Destroy(this);

        _isChanged = false;
    }

    public int Id { get { return _id; } set { _id = value; } }

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