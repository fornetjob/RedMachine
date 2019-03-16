using System.Collections.Generic;

namespace Assets.Scripts.Features.Pooling
{
    public class PoolService: IAttachContext, IService
    {
        #region Fields

        private Dictionary<string, IPool>
            _dict = new Dictionary<string, IPool>();

        private List<IPool>
            _pools = new List<IPool>();

        private Context
            _context;

        #endregion

        #region Listener

        public ListenerComposite<List<IPool>> OnPoolsChanged = new ListenerComposite<List<IPool>>();

        #endregion

        #region IAttachContext

        void IAttachContext.Attach(Context context)
        {
            _context = context;
        }

        #endregion

        #region Public methods

        public ComponentPool<T> Provide<T>()
            where T : IComponent, new()
        {
            string key = typeof(T).FullName;

            IPool pool;

            if (_dict.TryGetValue(key, out pool) == false)
            {
                pool = new ComponentPool<T>(_context);

                _pools.Add(pool);

                _dict.Add(key, pool);

                OnPoolsChanged.OnChanged(_pools);
            }

            return (ComponentPool<T>)pool;
        }

        public void DestroyAll(int id)
        {
            for (int i = 0; i < _pools.Count; i++)
            {
                var pool = _pools[i];

                if (pool.ContainsId(id))
                {
                    pool.GetById(id).Destroy();
                }
            }
        }

        #endregion
    }
}