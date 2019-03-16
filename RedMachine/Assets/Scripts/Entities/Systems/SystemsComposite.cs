using System.Collections.Generic;

namespace Assets.Scripts.Core
{
    public class SystemsComposite : IFixedUpdateSystem, IUpdateSystem, IFixedLateUpdateSystem
    {
        #region Fields

        private readonly List<IFixedUpdateSystem>
            _fixedUpdateSystems = new List<IFixedUpdateSystem>();

        private readonly List<IFixedLateUpdateSystem>
            _fixedLateUpdateSystems = new List<IFixedLateUpdateSystem>();

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

        public void Add(ISystem system)
        {
            if (system is IAttachContext)
            {
                ((IAttachContext)system).Attach(_context);
            }

            if (system is IStartSystem)
            {
                ((IStartSystem)system).OnStart();
            }

            if (system is IFixedLateUpdateSystem)
            {
                _fixedLateUpdateSystems.Add((IFixedLateUpdateSystem)system);
            }

            if (system is IFixedUpdateSystem)
            {
                _fixedUpdateSystems.Add((IFixedUpdateSystem)system);
            }

            if (system is IUpdateSystem)
            {
                _updateSystems.Add((IUpdateSystem)system);
            }
        }

        public void Remove(ISystem system)
        {
            if (system is IFixedLateUpdateSystem)
            {
                _fixedLateUpdateSystems.Remove((IFixedLateUpdateSystem)system);
            }

            if (system is IFixedUpdateSystem)
            {
                _fixedUpdateSystems.Remove((IFixedUpdateSystem)system);
            }

            if (system is IUpdateSystem)
            {
                _updateSystems.Remove((IUpdateSystem)system);
            }
        }

        #endregion

        #region IFixedUpdateSystem

        void IFixedUpdateSystem.OnFixedUpdate()
        {
            for (int i = 0; i < _fixedUpdateSystems.Count; i++)
            {
                _fixedUpdateSystems[i].OnFixedUpdate();
            }
        }

        #endregion

        #region IFixedLateUpdateSystem

        void IFixedLateUpdateSystem.OnFixedLateUpdate()
        {
            for (int i = 0; i < _fixedLateUpdateSystems.Count; i++)
            {
                _fixedLateUpdateSystems[i].OnFixedLateUpdate();
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
    }
}