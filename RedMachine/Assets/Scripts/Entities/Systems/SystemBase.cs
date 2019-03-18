public class SystemBase : ISystem
{
    #region Fields

    private int
        _order;

    #endregion

    #region ISystem implementation

    int ISystem.GetOrder()
    {
        return _order;
    }

    #endregion

    #region Public methods

    public ISystem SetOrder(int order)
    {
        _order = order;

        return this;
    }

    #endregion
}