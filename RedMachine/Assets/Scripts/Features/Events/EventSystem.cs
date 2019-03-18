namespace Assets.Scripts.Features.Events
{
    public class EventSystem : SystemBase, IUpdateSystem, IBeginSystem
    {
        #region Fields

        private Context
            _context;

        private ComponentPool<EventListenerComponent>
            _listeners;
        private ComponentPool<EventComponent>
            _events;

        #endregion

        #region IBeginSystem

        void IBeginSystem.OnBegin(Context context)
        {
            _context = context;

            _listeners = _context.services.pool.Provide<EventListenerComponent>();
            _events = _context.services.pool.Provide<EventComponent>();
        }

        #endregion

        #region IUpdateSystem

        void IUpdateSystem.OnUpdate()
        {
            if (_events.Items.Count == 0)
            {
                return;
            }

            for (int listenerIndex = 0; listenerIndex < _listeners.Items.Count; listenerIndex++)
            {
                var listener = _listeners.Items[listenerIndex];

                for (int i = 0; i < _events.Items.Count; i++)
                {
                    listener.value.OnEvent(_events.Items[i]);
                }
            }

            _events.DestroyAll();
        }

        #endregion
    }
}