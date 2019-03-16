namespace Assets.Scripts.Features.Times
{
    public class TimeScaleComponent: ComponentBase
    {
        private float 
            _scale;

        public float Scale
        {
            get
            {
                return _scale;
            }
            set
            {
                _scale = value;

                MarkAsChanged();
            }
        }
    }
}