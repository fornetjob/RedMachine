using Assets.Scripts.Features.Board;
using Assets.Scripts.Features.Unit;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Features.UI
{
    public class GameEndView : ViewBase, IListener<BoardStateComponent>
    {
        [SerializeField]
        private Text
            _winnerColorText = null;

        [SerializeField]
        private Text
            _simulationTimeText = null;

        public void OnChanged(BoardStateComponent value)
        {
            switch (value.Type)
            {
                case BoardStateType.GameEnd:
                    gameObject.SetActive(true);
                    break;
                default:
                    gameObject.SetActive(false);
                    break;
            }
        }

        public void Set(UnitType winnerType, float sumulationTime)
        {
            _winnerColorText.text = string.Format("WINNER COLOR: {0}", winnerType.ToString());
            _simulationTimeText.text = string.Format("SIMULATION TIME: {0:N2} seconds", sumulationTime);
        }
    }
}
