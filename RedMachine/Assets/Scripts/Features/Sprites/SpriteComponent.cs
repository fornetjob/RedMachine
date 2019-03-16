using UnityEngine;

namespace Assets.Scripts.Features.Sprites
{
    public class SpriteComponent : ComponentBase
    {
        public Sprite sprite;
        public int sortingOrder;

        public void Set(Sprite sprite, int sortingOrder = 0)
        {
            this.sprite = sprite;
            this.sortingOrder = sortingOrder;

            MarkAsChanged();
        }
    }
}