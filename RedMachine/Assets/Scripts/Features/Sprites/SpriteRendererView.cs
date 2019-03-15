using UnityEngine;

namespace Assets.Scripts.Features.Sprites
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class SpriteRendererView : ViewBase, IComponentListener<SpriteComponent>
    {
        private SpriteRenderer
            _rend;

        protected override void OnBegin()
        {
            _rend = gameObject.GetComponent<SpriteRenderer>();
        }

        public void OnChanged(SpriteComponent newValue)
        {
            _rend.sprite = newValue.sprite;
            _rend.sortingOrder = newValue.sortingOrder;
        }
    }
}