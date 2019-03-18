using UnityEngine;

namespace Assets.Scripts.Features.Move
{
    [System.Serializable]
    public class MoveComponent : ComponentBase
    {
        public Vector2 moveDirection;
        public float speed;

        public void Set(Vector2 moveDirection, float speed)
        {
            this.moveDirection = moveDirection;
            this.speed = speed;
        }
    }
}