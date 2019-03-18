using System.Collections.Generic;

namespace Assets.Scripts.Features.Pooling
{
    public class PoolSystem : SystemBase, IBeginSystem, IUpdateSystem, IListener<List<IPool>>
    {
        #region Fields

        private List<IPool>
            _pools;

        #endregion

        #region IBeginSystem

        void IBeginSystem.OnBegin(Context context)
        {
            _pools = context.services.pool.GetPools();

            context.services.pool.OnPoolsChanged.AddListener(this);
        }

        #endregion

        #region IListener<List<IPool>>

        void IListener<List<IPool>>.OnChanged(List<IPool> pools)
        {
            _pools = pools;
        }

        #endregion

        #region IUpdateSystem

        void IUpdateSystem.OnUpdate()
        {
            for (int i = 0; i < _pools.Count; i++)
            {
                _pools[i].ApplyChanges();
            }
        }

        #endregion
    }
}