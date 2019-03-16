namespace Assets.Scripts.Features.Scale
{
    public class RadiusComponent:ComponentBase
    {
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