using UnityEngine;

namespace Assets.Scripts.Features.Sprites
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class SpriteScaleView : ViewBase, IComponentListener<SpriteRadiusComponent>, IComponentListener<SpriteSizeComponent>
    {
        private SpriteRenderer
            _rend;

        protected override void OnBegin()
        {
            _rend = gameObject.GetComponent<SpriteRenderer>();
        }

        public void OnChanged(SpriteRadiusComponent newValue)
        {
            SetSize(Vector2.one * newValue.radius * 2);
        }

        public void OnChanged(SpriteSizeComponent newValue)
        {
            SetSize(newValue.size);
        }

        private void SetSize(Vector2 size)
        {
            _rend.drawMode = SpriteDrawMode.Sliced;
            _rend.size = size;
        }
    }
}
