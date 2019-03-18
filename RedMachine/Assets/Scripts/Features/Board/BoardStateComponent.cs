using System;
using UnityEngine;

namespace Assets.Scripts.Features.Board
{
    [Serializable]
    public class BoardStateComponent: ComponentBase
    {
        [SerializeField]
        private BoardStateType 
            _type;

        public BoardStateType Type
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
