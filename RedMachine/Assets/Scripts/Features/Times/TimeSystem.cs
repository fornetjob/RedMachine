namespace Assets.Scripts.Features.Times
{
    public class TimeSystem : SystemBase, IBeginSystem, IUpdateSystem
    {
        #region Feilds

        private TimeService
            _time;

        private ComponentPool<WaitComponent>
            _waits;

        private GameTimeComponent
            _gameTime;

        #endregion

        #region IBeginSystem

        void IBeginSystem.OnBegin(Context context)
        {
            _time = context.services.time;
            _waits = context.services.pool.Provide<WaitComponent>();

            _gameTime = context.services.pool.Provide<GameTimeComponent>().Create();
        }

        #endregion

        #region IUpdateSystem

        void IUpdateSystem.OnUpdate()
        {
            var timeScale = _time.TimeScale;
            var deltaTime = _time.GetDeltaTime();

            _gameTime.gameTime += deltaTime;

            if (timeScale != 0)
            {
                _gameTime.unscaledGameTime += deltaTime / timeScale;
            }

            for (int i = 0; i <_waits.Items.Count; i++)
            {
                var item = _waits.Items[i];

                if (item.IsPause)
                {
                    continue;
                }

                if (item.OnTick(deltaTime) == false)
                {
                    i--;
                }
            }
        }

        #endregion
    }
}
