using UnityEngine;

namespace Assets.Scripts.Features.Times
{
    public class TimeService:IService
    {
        #region Public methods

        public float TimeScale
        {
            get
            {
                return Time.timeScale;
            }
            set
            {
                Time.timeScale = value;
            }
        }

        public float GetDeltaTime()
        {
            return Time.deltaTime;
        }

        #endregion
    }
}
