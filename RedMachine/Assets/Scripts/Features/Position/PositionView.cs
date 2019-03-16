using UnityEngine;

namespace Assets.Scripts.Features.Position
{
    public class PositionView : ViewBase, IListener<PositionComponent>
    {
        #region Bindings

        private Transform
            _tr = null;

        #endregion

        #region Overriden methods

        protected override void OnBegin()
        {
            _tr = gameObject.transform;
        }

        #endregion

        #region IListener<PositionComponent>

        void IListener<PositionComponent>.OnChanged(PositionComponent newValue)
        {
            _tr.position = newValue.Position;
        }

        #endregion
    }
}
