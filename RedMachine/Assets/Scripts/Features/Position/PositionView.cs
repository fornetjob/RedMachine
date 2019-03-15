using UnityEngine;

namespace Assets.Scripts.Features.Position
{
    public class PositionView : ViewBase, IComponentListener<PositionComponent>
    {
        private Transform
            _tr = null;

        protected override void OnBegin()
        {
            _tr = gameObject.transform;
        }

        public void OnChanged(PositionComponent newValue)
        {
            _tr.position = newValue.pos;
        }
    }
}
