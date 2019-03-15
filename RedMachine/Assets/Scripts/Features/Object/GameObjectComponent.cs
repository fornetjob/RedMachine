using Assets.Scripts.Features.Pooling;
using UnityEngine;

namespace Assets.Scripts.Features.Object
{
    public class GameObjectComponent:IComponent, IDestroy
    {
        public GameObject obj;

        void IDestroy.Destroy()
        {
            GameObject.Destroy(obj);
        }
    }
}
