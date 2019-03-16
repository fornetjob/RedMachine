using UnityEngine;

namespace Assets.Scripts.Features.Object
{
    public class GameObjectComponent: ComponentBase
    {
        public GameObject obj;

        protected override void OnDestroy()
        {
            GameObject.Destroy(obj);
        }
    }
}