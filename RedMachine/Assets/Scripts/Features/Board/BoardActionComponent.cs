namespace Assets.Scripts.Features.Board
{
    public class BoardActionComponent: ComponentBase
    {
        private BoardActionType 
            _type;

        public BoardActionType Type
        {
            get
            {
                return _type;
            }
            set
            {
                _type = value;

                MarkAsChanged();
            }
        }
    }
}
