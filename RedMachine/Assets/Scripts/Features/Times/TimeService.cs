using UnityEngine;

namespace Assets.Scripts.Features.Times
{
    public class TimeService:IService
    {
        public Wait WaitTo(float seconds, bool isAutoReset)
        {
            return new Wait(this, seconds, isAutoReset);
        }

        public float GetTimeSinceLevelLoad()
        {
            return Time.timeSinceLevelLoad;
        }

        public float GetFixedDeltaTime()
        {
            return Time.fixedDeltaTime;
        }

        public float GetDeltaTime()
        {
            return Time.deltaTime;
        }
    }
}
