using UnityEngine;

namespace Assets.Scripts.Features.Scale
{
    public class SizeComponent : ComponentBase
    {
        private Vector2
            _size;

        public Vector2 Size
        {
            get
            {
                return _size;
            }
            set
            {
                _size = value;

                MarkAsChanged();
            }
        }
    }
}