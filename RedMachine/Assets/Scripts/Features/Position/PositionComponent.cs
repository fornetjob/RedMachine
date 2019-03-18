using UnityEngine;

namespace Assets.Scripts.Features.Position
{
    [System.Serializable]
    public class PositionComponent : ComponentBase
    {
        #region Fields

        [SerializeField]
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