using UnityEngine;

namespace Assets.Scripts.Features.Position
{
    public class PositionComponent : ComponentBase
    {
        #region Fields

        private Vector2
            _pos;

        #endregion

        public Vector2 Position
        {
            get
            {
                return _pos;
            }
            set
            {
                _pos = value;

                MarkAsChanged();
            }
        }
    }
}