using UnityEngine;

public abstract class ViewBase : MonoBehaviour
{
    #region Fields

    protected ComponentPool<EventComponent>
        _eventPool;

    #endregion

    #region Public methods

    public void BeginView(ComponentPool<EventComponent> eventPool)
    {
        _eventPool = eventPool;

        OnBegin();
    }

    #endregion

    #region Virtual methods

    protected virtual void OnBegin()
    {

    }

    #endregion
}