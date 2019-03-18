using Assets.Scripts.Features.Bounces;
using Assets.Scripts.Features.Serialize;
using Assets.Scripts.Features.Move;
using Assets.Scripts.Features.Unit;

using UnityEngine;

namespace Assets.Scripts.Features.Board
{
    public class BoardSystem : SystemBase, IBeginSystem, IListener<BoardStateComponent>
    {
        #region Factories

        private BoardFactory
            _boardFactory;

        #endregion

        #region Fields

        private Context
            _context;

        private GameConfig
            _config;

        #endregion

        #region IBeginSystem

        void IBeginSystem.OnBegin(Context context)
        {
            _context = context;

            _boardFactory = new BoardFactory(context);

            Physics.autoSimulation = false;
            Physics2D.autoSimulation = false;

            _config = _context.services.serialize.GetGameConfig();

            _boardFactory.Create(_config.GetBoardSize())
                .AddListener(this);
        }

        #endregion

        #region IListener<BoardActionComponent>

        void IListener<BoardStateComponent>.OnChanged(BoardStateComponent newValue)
        {
            switch (newValue.Type)
            {
                case BoardStateType.Add:
                    _context.systems.Get<UnitAddSystem>().SetActive(true);

                    _context.systems.Get<MoveSystem>().IsActive = false;
                    _context.systems.Get<BounceSystem>().IsActive = false;
                    break;
                case BoardStateType.Move:
                    _context.systems.Get<UnitAddSystem>().SetActive(false);

                    _context.systems.Get<MoveSystem>().IsActive = true;
                    _context.systems.Get<BounceSystem>().IsActive = true;
                    break;
            }
        }

        #endregion
    }
}
