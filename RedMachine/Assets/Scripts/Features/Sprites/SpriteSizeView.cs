using Assets.Scripts.Features.Scale;
using UnityEngine;

namespace Assets.Scripts.Features.Sprites
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class SpriteSizeView : ViewBase, IListener<SizeComponent>
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

        #region IListener<SizeComponent>

        void IListener<SizeComponent>.OnChanged(SizeComponent newValue)
        {
            _rend.drawMode = SpriteDrawMode.Sliced;
            _rend.size = newValue.Size;
        }

        #endregion
    }
}