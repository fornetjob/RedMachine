using Assets.Scripts.Features.Bounces;
using Assets.Scripts.Features.Serialize;
using Assets.Scripts.Features.Move;
using Assets.Scripts.Features.Unit;
using UnityEngine;

namespace Assets.Scripts.Features.Board
{
    public class BoardSystem : IAttachContext, IStartSystem, IListener<BoardActionComponent>
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

        #region IAttachContext

        void IAttachContext.Attach(Context context)
        {
            _context = context;

            _boardFactory = new BoardFactory(context);
        }

        #endregion

        #region IListener<BoardActionComponent>

        void IListener<BoardActionComponent>.OnChanged(BoardActionComponent newValue)
        {
            switch (newValue.Type)
            {
                case BoardActionType.Add:
                    _context.systems.Add(new UnitAddSystem());
                    break;
                case BoardActionType.Move:
                    _context.systems.Add(new MoveSystem());
                    _context.systems.Add(new BounceSystem());
                    break;
            }
        }

        #endregion

        #region IStartSystem

        void IStartSystem.OnStart()
        {
            Physics.autoSimulation = false;
            Physics2D.autoSimulation = false;

            _config = _context.services.serialize.GetGameConfig();

            _boardFactory.Create(_config.GetBoardSize())
                .AddListener(this);
        }

        #endregion
    }
}
