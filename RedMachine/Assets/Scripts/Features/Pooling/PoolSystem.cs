using System.Collections.Generic;

namespace Assets.Scripts.Features.Pooling
{
    public class PoolSystem : IFixedLateUpdateSystem, IAttachContext, IListener<List<IPool>>
    {
        #region Fields

        private List<IPool>
            _pools;

        #endregion

        #region IAttachContext

        void IAttachContext.Attach(Context context)
        {
            context.services.pool.OnPoolsChanged.AddListener(this);
        }

        #endregion

        #region IListener<List<IPool>>

        void IListener<List<IPool>>.OnChanged(List<IPool> pools)
        {
            _pools = pools;
        }

        #endregion

        #region IFixedLateUpdateSystem

        void IFixedLateUpdateSystem.OnFixedLateUpdate()
        {
            for (int i = 0; i < _pools.Count; i++)
            {
                _pools[i].ApplyChanges();
            }
        }

        #endregion
    }
}