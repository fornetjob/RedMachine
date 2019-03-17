using System.Collections.Generic;

namespace Assets.Scripts.Core
{
    public class SystemsComposite : IUpdateSystem, ILateUpdateSystem
    {
        #region Fields

        private readonly List<ISystem>
            _systems = new List<ISystem>();

        private readonly List<ILateUpdateSystem>
            _lateUpdateSystems = new List<ILateUpdateSystem>();

        private readonly List<IUpdateSystem>
            _updateSystems = new List<IUpdateSystem>();

        private readonly Context
            _context;

        #endregion

        #region ctor

        public SystemsComposite(Context context)
        {
            _context = context;
        }

        #endregion

        #region Public methods

        public T Get<T>()
            where T:ISystem
        {
            return (T)_systems.Find(p => p is T);
        }

        public void Add(ISystem system)
        {
            _systems.Add(system);

            if (system is IStartSystem)
            {
                ((IStartSystem)system).OnStart(_context);
            }

            if (system is ILateUpdateSystem)
            {
                _lateUpdateSystems.Add((ILateUpdateSystem)system);
            }

            if (system is IUpdateSystem)
            {
                _updateSystems.Add((IUpdateSystem)system);
            }
        }

        public void Remove(ISystem system)
        {
            _systems.Remove(system);

            if (system is ILateUpdateSystem)
            {
                _lateUpdateSystems.Remove((ILateUpdateSystem)system);
            }

            if (system is IUpdateSystem)
            {
                _updateSystems.Remove((IUpdateSystem)system);
            }
        }

        #endregion

        #region IUpdateSystem

        void IUpdateSystem.OnUpdate()
        {
            for (int i = 0; i < _updateSystems.Count; i++)
            {
                _updateSystems[i].OnUpdate();
            }
        }

        #endregion

        #region ILateUpdateSystem

        void ILateUpdateSystem.OnLateUpdate()
        {
            for (int i = 0; i < _lateUpdateSystems.Count; i++)
            {
                _lateUpdateSystems[i].OnLateUpdate();
            }
        }

        #endregion
    }
}