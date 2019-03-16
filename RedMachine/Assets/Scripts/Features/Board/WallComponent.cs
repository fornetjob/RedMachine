using UnityEngine;

namespace Assets.Scripts.Features.Board
{
    public class WallComponent: ComponentBase
    {
        public Vector2 normal;
        public Bounds bound;

        public void Set(Vector2 normal, Bounds bound)
        {
            this.normal = normal;
            this.bound = bound;
        }
    }
}