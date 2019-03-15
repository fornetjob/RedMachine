using Assets.Scripts.Features.Times;

public class Wait
{
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

    public Wait(TimeService time, float timeInterval, bool isAutoReset)
    {
        _time = time;

        _timeInterval = timeInterval;
        _isAutoReset = isAutoReset;

        Next();
    }

    private void Next()
    {
        _endTime = _time.GetTimeSinceLevelLoad() + _timeInterval;

        _isCheck = false;
    }

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
                Next();
            }

            return true;
        }

        return false;
    }
}