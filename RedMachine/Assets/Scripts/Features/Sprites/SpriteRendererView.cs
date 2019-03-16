using UnityEngine;

namespace Assets.Scripts.Features.Sprites
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class SpriteRendererView : ViewBase, IListener<SpriteComponent>
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

        #region IListener<SpriteComponent>

        public void OnChanged(SpriteComponent newValue)
        {
            _rend.sprite = newValue.sprite;
            _rend.sortingOrder = newValue.sortingOrder;
        }

        #endregion
    }
}