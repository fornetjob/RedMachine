namespace Assets.Scripts.Features.Times
{
    public class TimeSystem : IStartSystem, IUpdateSystem
    {
        private TimeService
            _time;

        private ComponentPool<WaitComponent>
            _waits;

        public void OnStart(Context context)
        {
            _time = context.services.time;
            _waits = context.services.pool.Provide<WaitComponent>();
        }

        public void OnUpdate()
        {
            var deltaTime = _time.GetDeltaTime();

            for (int i = 0; i <_waits.Items.Count; i++)
            {
                if (_waits.Items[i].OnTick(deltaTime) == false)
                {
                    i--;
                }
            }
        }
    }
}
