using Assets.Scripts.Features.Scale;
using UnityEngine;

namespace Assets.Scripts.Features.Sprites
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class SpriteRadiusView : ViewBase, IListener<RadiusComponent>
    {
        #region Fields

        private SpriteRenderer
            _rend;

        #endregion

        #region Overriden methods

        protected override void OnBegin()
        {
            _rend = gameObject.GetComponent<SpriteRenderer>();
        }

        #endregion

        #region IListener<RadiusComponent>

        void IListener<RadiusComponent>.OnChanged(RadiusComponent newValue)
        {
            _rend.drawMode = SpriteDrawMode.Sliced;
            _rend.size = Vector2.one * newValue.Radius * 2;
        }

        #endregion
    }
}