using System.Collections.Generic;

namespace Assets.Scripts.Core
{
    public class SystemsComposite : SystemBase, IUpdateSystem
    {
        #region Fields

        private readonly List<ISystem>
            _systems = new List<ISystem>();

        private readonly List<IUpdateSystem>
            _updateSystems = new List<IUpdateSystem>();

        private readonly Context
            _context;

        private readonly SystemComparer<IUpdateSystem>
            _updateSystemComparer = new SystemComparer<IUpdateSystem>();

        #endregion

        #region ctor

        public SystemsComposite(Context context)
        {
            _context = context;
        }

        #endregion

        #region Public methods

        public bool IsExist<T>()
        {
            return _systems.FindIndex(p => p is T) != -1;
        }

        public T Get<T>()
            where T:ISystem
        {
            return (T)_systems.Find(p => p is T);
        }

        public void Add(ISystem system)
        {
            _systems.Add(system);

            if (system is IBeginSystem)
            {
                ((IBeginSystem)system).OnBegin(_context);
            }

            if (system is IUpdateSystem)
            {
                _updateSystems.Add((IUpdateSystem)system);
                _updateSystems.Sort(_updateSystemComparer);
            }
        }

        public void Remove(ISystem system)
        {
            _systems.Remove(system);

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
    }
}