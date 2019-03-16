using Assets.Scripts.Features.Times;

public class Wait
{
    #region Fields

    private TimeService
        _time;

    private bool
        _isAutoReset;

    private float
        _timeInterval;

    private float
        _endTime;

    private bool
        _isCheck;

    #endregion

    #region ctor

    public Wait(TimeService time, float timeInterval, bool isAutoReset)
    {
        _time = time;

        _timeInterval = timeInterval;
        _isAutoReset = isAutoReset;

        Reset();
    }

    #endregion

    #region Public methods

    public bool IsCheck()
    {
        if (_isCheck)
        {
            return false;
        }

        if (_endTime < _time.GetTimeSinceLevelLoad())
        {
            _isCheck = true;

            if (_isAutoReset)
            {
                Reset();
            }

            return true;
        }

        return false;
    }

    #endregion

    #region Private methods

    private void Reset()
    {
        _endTime = _time.GetTimeSinceLevelLoad() + _timeInterval;

        _isCheck = false;
    }

    #endregion
}