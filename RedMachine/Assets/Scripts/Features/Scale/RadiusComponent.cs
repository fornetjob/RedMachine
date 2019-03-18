using UnityEngine;

namespace Assets.Scripts.Features.Scale
{
    [System.Serializable]
    public class RadiusComponent:ComponentBase
    {
        [SerializeField]
        private float 
            _radius;

        public float Radius
        {
            get
            {
                return _radius;
            }
            set
            {
                _radius = value;

                MarkAsChanged();
            }
        }
    }
}