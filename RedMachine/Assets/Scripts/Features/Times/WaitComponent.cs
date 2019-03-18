public class WaitComponent : ComponentBase
{
    #region Fields

    private float
        _currentTime;
    private float
        _timeInterval;
    private bool
        _isAutoReset;
    private bool
        _isTimeEnd;

    #endregion

    #region Properties

    public bool IsPause;

    #endregion

    #region Public methods

    public bool OnTick(float deltaTime)
    {
        if (_isTimeEnd)
        {
            Destroy();

            return false;
        }

        _currentTime += deltaTime;

        if (_currentTime >= _timeInterval)
        {
            _isTimeEnd = true;

            MarkAsChanged();

            if (_isAutoReset)
            {
                Reset();
            }
        }

        return true;
    }

    public void Set(float timeInterval, bool isAutoReset)
    {
        _timeInterval = timeInterval;
        _isAutoReset = isAutoReset;

        Reset();
    }

    #endregion

    #region Private methods

    private void Reset()
    {
        _currentTime = 0;
        _isTimeEnd = false;
    }

    #endregion
}